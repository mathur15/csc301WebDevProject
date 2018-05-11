const mongoose = require('mongoose');
//const passportLocalMongoose = require('passport-local-mongoose');

const VideoSchema = new mongoose.Schema({
	course: {type: String, required: true}, 
	date: {type: Date, required: true},
	lecture: {type: Number, required:true},
	upvotes: {type: Number, required:true, default: 0},
	user: {type:mongoose.Schema.Types.ObjectId, 
		   ref: "User"},
	link: {type:String, required:true},
	thumbnail: {type:String, required:false}
});

//VideoSchema.plugin(passportLocalMongoose);
module.exports = mongoose.model('Video', VideoSchema);


// course 		lecture			upvotes		user_id		link
// "csc384"	"lecture 1" 	100
// "csc384"	"lecture 1"		200
// "csc384"	"lecture 1"		300
// "csc384"	"lecture 1"		400
// "csc384"	"lecture 1"		500
// "csc384"	"lecture 2"		350
// "csc384"	"lecture 2"		200
// "csc384"	"lecture 2"		100
// "csc384"	"lecture 3"		260

// SELECT course, lecture, max(upvotes), count(*) FROM VideoSchema
// GROUP BY course, lecture
// WHERE course = "csc384"