var mongoose = require("mongoose");
var User =  require("./schema/user");
var Videos = require("./schema/videos");

var firstUser = new User({
	username: "adlakhap",
	password: "chakka"
})
var secondUser = new User({
	username: "chakka",
	password: "prav"
})
var thirdUser = new User({
	username: "awesome",
	password: "som"
})

var video1 = new Videos({
	// title: "Excuses - I Can't Come To Your Party",
	course: "CSC384",
	lecture: 1,
    date: "Sun Mar 25 2018 19:26:18 GMT-0400 (Eastern Standard Time)",
    upvotes: 47,
    user: firstUser._id,
    link: "https://www.youtube.com/embed/b872wYju_FY"
})

var video2 = new Videos({
	// title: "What to do when someone asks, What are you looking at?",
    course: "CSC384",
	lecture: 2,
	date: "Sun Mar 25 2018 19:34:02 GMT-0400 (Eastern Standard Time)",
	upvotes: 27,
	user: secondUser._id,
	link: "https://www.youtube.com/embed/Y6yYKP22xHM"
})

var video3 = new Videos({
	// title: "WHAT TO DO WHEN YOU GET JUMPED",
	course: "CSC384",
	lecture: 2,
	date: "Fri Mar 30 2018 21:01:57 GMT-0400 (Eastern Standard Time)",
	upvotes: 30,
	user: thirdUser._id,
	link: "https://www.youtube.com/embed/VVTWE7ysFoI"
})

var videoData = [video1,video2,video3];
var userData = [firstUser,secondUser,thirdUser];

function seedDB(){
	//remove all users from the database
	User.remove({}, function(err){
		if(err){
			console.log(err);
		}
		console.log("removed users");
		// add a couple of users
		userData.forEach(function(seed){
			User.create(seed, function(err, user){
				if(err){
					console.log(err);
				}
				else {
					console.log("added a user: " + user.username);
				}
			});
		});
	});
	//remove all videos from the database
	Videos.remove({}, function(err){
		if(err){
			console.log(err);
		}
		console.log("removed videos");
		// add a couple of videos
		videoData.forEach(function(seed){
			Videos.create(seed, function(err, video){
				if(err){
					console.log(err);
				}
				else {
					console.log("added a video: " + video.title);
				}
			});
		});
	});
}

module.exports = seedDB;
