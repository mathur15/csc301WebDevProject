// setup
const express = require('express'),
app = express(),
bodyParser = require('body-parser'),
passport = require("passport"),
LocalStrategy = require("passport-local").Strategy,
mongoose = require('mongoose'),
methodOverride = require('method-override'),
routes = require('./routes/routes.js'),
User = require('./schema/user'),
http = require("http"),
Video = require("./schema/videos")
seedDB = require("./seeds"),
logic = require("./routes/logic"),
fileUpload = require('express-fileupload'),
ffmpeg = require('ffmpeg');

// mongo ds113169.mlab.com:13169/relec -u relec -p relec
// to connect on mongo
app.use(require("express-session")({
  secret: "Secret Bitch",
  resave: false,
  saveUninitialized: false,
  cookie:{ path: '/', httpOnly: true, secure: false, maxAge: null }
}));
app.use(passport.initialize());
app.use(passport.session());
app.use(fileUpload());



// LOGIN VERIFICATION
var LOCAL_STRATEGY_CONFIG = {
  usernameField: 'username',
  passwordField: 'password',
  session: false,
  passReqToCallback: true
};

function _onLocalStrategyAuth(req, username, password, done){
  User.findOne({username: username}, function(err, user){
    if (err){
      return done(err);
    }
    if(!user){
      return done(null, false);
    }
    if (user.password != password){
      console.log("dont match")
      return done(null, false);
    }
    return done(null, user);
  });
}

passport.use(new LocalStrategy(LOCAL_STRATEGY_CONFIG, _onLocalStrategyAuth));

passport.serializeUser(User.serializeUser());
passport.deserializeUser(User.deserializeUser());

mongoose.connect("mongodb://relec:relec@ds113169.mlab.com:13169/relec");
app.use(express.json());
app.use(bodyParser.urlencoded({ extended: true, limit:'50mb'}));
app.set("view engine", "ejs");
app.use(express.static(__dirname + "/views"));
app.use('/videos',express.static(__dirname + "/videos"));
app.use(methodOverride('_method'));
seedDB();


//middleware for all code
app.use(function(req, res, next){
    // user would either be undefined or the signed in user
    res.locals.currentUser = req.user;
    next();
  });

//routes
app.get("/",function(req,res){
  res.render("index", {home: true});
});


//USED TO BE app.post("/") - changed to app.get("/browse")
app.post("/browse", (req,res) => {
  console.log(req.body.search);
  var search_course = req.body.search;
  //console.log(search_course);

  //find the best video for each lecutre in the class that we searched for
  Video.aggregate([
    {$match: { course: { $regex: search_course, $options: 'i' }}},
    {$group:
      {_id: { course: "$course", lecture: "$lecture" },
      maxUpvotes: { $max:"$upvotes" },
      count: { $sum: 1 }}},
    {$sort: { lecture: -1 }} // To show latest lectures at the top
    ],
    //callback function
    //once we got the best videos per lecture, we want to get all the metadata associated with them ;)
    function(err,bestVideos){
      if(err){
        throw err;
      }
      console.log(bestVideos);


        var new_results = []  //keeps track of the video objects
        
        //Asynchronously looks for the our best videos
        function asyncFunction (result, callback) {
          console.log("result: ", result._id.course, result._id.lecture, result.maxUpvotes);
          Video.findOne({course: result._id.course, lecture : result._id.lecture, upvotes: result.maxUpvotes}, function(err2, video){
            if (err2){
              console.log("bust");
            }
            User.findOne({_id:video.user},function(err,foundUser){
              if(err){
                console.log(err)
              }
              video.username = foundUser.username;
              video.count = result.count;
              // console.log(video);
              new_results.push(video);
              callback();  //callback the previous one
            });
          });
        }

        //make a promise chain so that we search through all the lectures - we do this so that we render the browse page after we get all the videos
        var requests = bestVideos.reduce((promiseChain, result) => {
          return promiseChain.then(() => new Promise((callback) => {
            asyncFunction(result, callback);
          }), () => {false});
        }, Promise.resolve());

        //render the page once we finish getting all the videos
        requests.then(() => {res.render("browse",{search:search_course, result:new_results, home: false})});
      });
});

// Redirect to homepage instead of browse to get a search query from user
app.get('/browse', (req,res) => {
  res.redirect('/');
});

// app.get('/myVideos', (req, res) => {
//   res.render('myVideos', {home: false});
// });

app.get('/lecture', (req,res) =>{
  res.redirect('/');
});

app.post('/lecture', (req,res) =>{
  var search_course = req.body.course;
  var search_lecture = req.body.lecture;
  var user_id = req.body.user;
  var purchased = false;
  console.log(search_course);
  console.log(search_lecture);

  var videos;

  Video.findOne({course: search_course, lecture: search_lecture, user: user_id}, 
    function(err, mainVid){
      if(err){
        console.log(err);
      }
      console.log("Main video:");
      console.log(mainVid);
      Video.find({course: search_course, lecture: search_lecture}).sort({ upvotes: -1 }).exec(
        function(err, otherVids) {
          if(err){
            console.log(err);
          }
          function userLookup(vid, callback) {
            // console.log(vid)
            User.findOne({_id:vid.user},function(err,foundUser){
              if(err){
                console.log(err);
              }
              vid.username = foundUser.username;
              callback();  //callback the previous one
            });
          }

          var requests = otherVids.reduce((promiseChain, result) => {
              return promiseChain.then(() => new Promise((callback) => {
                userLookup(result, callback);
              }), () => {false});
          }, Promise.resolve());

          requests.then(() => {
            // Remove display vid from other vids 
            removeInd = -1;
            otherVids.forEach(function(vid, i) {
              if (removeInd == -1 && JSON.stringify(vid) == JSON.stringify(mainVid)){
                removeInd = i;
              }
            });
            console.log("Other vids before splice:");
            console.log(otherVids);

            if(removeInd != -1) {
              var tossedVid = otherVids.splice(removeInd, 1);
              mainVid.username = tossedVid[0].username;
            }

            // Check if user has purchased this lecture already
            if(res.locals.currentUser){  
              User.findById(res.locals.currentUser._id, 
                function(err,foundUser){
                  if(err){
                    console.log(err);
                  }
                  // console.log(foundUser);
                  foundUser.purchased.forEach(function(purchasedLec){
                    if(purchasedLec.course == search_course && purchasedLec.lecture == search_lecture){
                      purchased = true;
                    }
                  });
                  console.log("After removing main vid:")
                  console.log(otherVids)
                  console.log("Lectre is purchased already:")
                  console.log(purchased)
                  res.render('lecture', { home: false, mainVideo: mainVid, otherVideos: otherVids, purchased: purchased });
                });
            } else {
              res.render('lecture', { home: false, mainVideo: mainVid, otherVideos: otherVids, purchased: purchased });
            }
          });
        });
    }); //end of mianVid callback
});

app.get('/myUploads', (req, res) => {
  res.render('myUploads', {home: false});
});

//route to display the videos purchased and uploaded by currentUser
app.get("/myVideos", (req,res) =>{
  //find videos according to the user logged in
  //console.log(res.locals.currentUser)
  //find purchased videos
  User.findById(res.locals.currentUser._id,function(err,foundUser){
    //console.log(foundUser);
    var new_results = []
    //retrieving all the purchased videos
    foundUser.purchased.forEach(function(vid){
          Video.aggregate([
              {$match: { course: vid.course, lecture: vid.lecture}},
              {$group:
                {_id: { course: "$vid.course", lecture: "$vid.lecture" },
                maxUpvotes: { $max:"$upvotes" },
                count: { $sum: 1 }}},
              {$sort: { lecture: -1 }} // To show latest lectures at the top
              ],
              //callback function
              //once we got the best videos per lecture, we want to get all the metadata associated with them ;)
              function(err,bestVideo){
                if(err){
                  throw err;
                }
                console.log(bestVideo);

                Video.findOne({course: bestVideo._id.course, lecture : bestVideo._id.lecture, upvotes: bestVideo.maxUpvotes}, function(err2, video){
                  if (err2){
                    console.log("bust");
                  }
                  User.findOne({_id:video.user},function(err,foundUser){
                    if(err){
                      console.log(err)
                    }
                    video.username = foundUser.username;
                    video.count = bestVideo.count;
                    // console.log(video);
                    new_results.push(video);
                  });
                });
              })
  });
  res.render("myVideos",{result:new_results, home: false})
 });
});

//login logic
app.get("/logout", function(req, res){
 req.logout();
 res.redirect("/");
});

app.get('/myProfile', (req, res) => {
  res.render('myProfile', {home: false});
});

//show login form
app.get('/login', function(req, res){
  res.render('login');
});

//when user clicks on purchase
app.post("/addVideo", (req,res) =>{
  var course = req.body.course;
  var lecture = req.body.lecture;
  User.findById(res.locals.currentUser._id, function(err,foundUser){
    foundUser.purchased.push({
      course: course,
      lecture: lecture
    })
    foundUser.credits = foundUser.credits - 1;
    foundUser.save();
    console.log(foundUser);
    // console.log()
    res.status(200).send(foundUser.credits + "");
  })
});

app.post('/login', passport.authenticate("local", 
{   
        //middleware
        successRedirect: "/",
        failureRedirect: "login"
      }), function(req, res){
  //console.log(req.body.username, req.body.password);
});

app.post('/upload', function(req, res) {
  if (!req.files)
    return res.status(400).send('No files were uploaded.');
  if(!req.body.course || !req.body.lecture){
    return res.status(400).send('No Title or course or lecture');
  }
  var lecture = req.files.video;
  var title = Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5);
  lecture.mv('./raw_videos/'+title, function(err) {
    if (err)
      return res.status(500).send(err);
    new ffmpeg('./raw_videos/'+title).then(function(video){
      video
      .setVideoSize("1280x?",true, true, "000000")
      .setVideoCodec('h264')
      .setAudioCodec('mp2')
      .save('./videos/'+title+'.mp4', function (error, file) {
        if(error)
          console.log(error);
        new ffmpeg('./videos/'+title+'.mp4').then(function(conv){
          conv.fnExtractFrameToJPG('./videos/', {start_time:3,number:1}, function(error, files){
            User.findOne({username:req.body.username}, function(err,user){
              req.user = user;
              Video.create({
                course: req.body.course,
                lecture: parseInt(req.body.lecture),
                date: new Date(),
                upvotes: 0,
                user: req.user._id,
                link: "/videos/"+title+'.mp4',
                thumbnail:"/videos/"+title+'_1.jpg'
              }, 
              function (err, newVideo) {
                if(err){
                  console.log(err);
                }
                User.findById(user._id, function (err, uploader) {
                  if(err){
                    console.log(err);
                  }
                  uploader.uploads.push(newVideo._id);
                  uploader.credits += 1;
                  uploader.save();
                  res.status(200).send("Cool.");
                });
              });
            });
          });
        });
      });
    });
  });

});

// Checks if user is loggedIn
function isLoggedIn(req, res, next){
  if(req.isAuthenticated()){
    return next();
  }
  res.redirect("/login");
}


const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log("Server started");
});