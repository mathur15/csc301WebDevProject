var express = require('express'),
    router = express.Router(),
    passport = require('passport'),
    User = require('../schema/user');

// const viewDest = String(__dirname).replace('routes', 'views');
// root route

router.get('/', (req, res) => {
  res.render('./views/home');
});

router.get('/browse', (req, res) => {
  res.render('browse');
});

router.get('/myVideos', (req, res) => {
  res.render('myVideos');
});

router.get('/myUploads', (req, res) => {
  res.render('myUploads');
});

router.get('/myProfile', (req, res) => {
  res.render('myProfile');
});

// show register form
router.get('/register', (req, res) => {
  res.render('register');
});

router.get('/courses', function(req,res){
	
});

router.get('/lecture', (req, res) => {
    res.render('lecture');
});

// // handle sign up logic
// router.post('/register', (req, res) => {
//   const newUser = new User({ username: req.body.username, password: req.body.password });
//   User.register(newUser, req.body.password, (err) => {
//     if (err) {
//       res.send('register');
//     }
//     passport.authenticate('local')(req, res, () => {
//       req.flash('success', 'Welcome to Re-Lec!');
//       res.redirect('/');
//     });
//   });
// });

// // show login form
// router.get('/login', (req, res) => {
//   res.render('login');
// });

// // handling login logic
// router.post('/login', passport.authenticate(
//   'local',
//   {
//     successRedirect: '/',
//     failureRedirect: '/login',
//     failureFlash: true,

//   },
// ));

// // logout route
// router.get('/logout', (req, res) => {
//   req.logout();
//   req.flash('success', 'Successful Logout!');
//   res.redirect('/');
// });


module.exports = router;
