const mongoose = require('mongoose');
const passportLocalMongoose = require('passport-local-mongoose');

const ProfSchema = new mongoose.Schema({
	name: {type: String, required: true},
	courses: [String]
});

ProfSchema.plugin(passportLocalMongoose);
module.exports = mongoose.model('Prof', ProfSchema);