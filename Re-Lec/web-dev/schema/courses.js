const mongoose = require('mongoose');
const passportLocalMongoose = require('passport-local-mongoose');

const CourseSchema = new mongoose.Schema({
	name: {type: String, required: true},
	dept: {type: String, required: true},
	cnum: {type: String, required: true}
});

CourseSchema.plugin(passportLocalMongoose);
module.exports = mongoose.model('Course', CourseSchema);