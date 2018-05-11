const unzip = require('unzip');
const mongoose = require('mongoose');
var User = require("../schema/user");
var Videos = require("../schema/videos");
var Courses = require("../schema/courses");
var Profs = require("../schema/courses");

//route to the directory holding videos
const rawVideosDirectory = '../raw_videos'
const videoDirectory = '../videos'

function addVideo(username, title, course, lecture, date){
	User.find({username: username}, function(err, user){
		if(err){
			console.log(err);
		}
		var newVideo = {name: name, course: course, lecture: lecture, date: date, user: user._id}
		Videos.create(newVideo, function(err, newlyCreated){
			if(err){
				console.log(err);
			}
			user.videos.push(newlyCreated._id);
			user.tokens += 1;
		});
	});
}


function userCanWatch(user, video){
	return user.videos.indexOf(video.id) != -1;
}


function accessVideo(username, video){
	User.find({username: username}, function(err, user){
		if(err){
			console.log(err);
		}
		if (user.credits > 0){
			user.credits -= user.credits;
			user.videos.push(video._id);
			alert("successfully added video");
		}
		else {
			alert("Not enough credits for video");
		}
	});
}


// iterates over all videos in the Videos schema and returns a list of JSON objects
// that are videos in course: course and lecture: lecture
function findVideos(course, lecture, callback) {
	var videos = [];

	// find video schema
	Videos.find({}, function(err, vids) {
		if(err){
			console.log(err);
		}
		else {
			// iterate over Video schema
			vids.forEach(function(video){
				if (video.course === course && video.lecture === lecture){
					videos.push(video.toJSON({ virtuals: true }));
				}
			});
		}
		callback(videos);
	});

}

// Finds the best video for the course
function findBestVideo(course, callback) {
	var video = Videos.find({course}).sort({upvotes:-1}).limit(1).pretty().toJSON({virtuals : true});
	console.log(video);
	callback(videos);
	
}

// Get all courses available
function getAllCourseNames(callback){
	var courses = {};
	Courses.find({}, function(err, cours) {
		if(err){
			console.log(err);
		}
		else {
			cours.forEach(function(course){
				courses[course.name] = course.dept + course.cnum;
			});
		}
		callback(courses);
	});
}

// Get all professors teaching
function getAllProfs(callback){
	var professors = [];
	Profs.find({}, function(err, profs){
		if(err){
			console.log(err);
		}
		else {
			profs.forEach(function(prof){
				professors.push(prof.name);
			});
		}
		callback(professors);
	});
}

// Get all student utorIDs
function getAllUsersIDs(){
	var allUsers = [];
	Users.find({}, function(err, users){
		if (err){
			console.log(err);
		}
		else {
			users.forEach(function(user){
				allUsers.push(user.utorid);
			});
		}
		callback(allUsers);
	});
}

// Add user to the schema
function addUser(username, password){
	var newUser = {username: username, password: password}
	User.create(newUser, function(err, newlyCreated){
		if(err){
			console.log(err);
		}
	});
}

// get courses taught by a professors
function getCoursesTaught(prof){

}

//meant to take a video file, unzip it, and store it in the server
function uploadVideo(fileName){
	var rawVideoFilePath = rawVideosDirectory + '/' + fileName;
	var videoFilePath = videoDirectory + '/' + fileName;
	var readStream = fs.createReadStream(rawVideoFilePath);
	var writeStream = fstream.Writer(videoDirectory);
	 
	readStream
		.pipe(unzip.Parse())
		.pipe(writeStream)
}

//return the src file to load on the html
function getVideoSrc(fileName){
	var videoFilePath = videoDirectory + '/' + fileName;
	return videoFilePath;
}