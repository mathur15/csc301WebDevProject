const mongoose = require('mongoose');
const passportLocalMongoose = require('passport-local-mongoose');

var UserSchema = new mongoose.Schema({
  username: { type: String, required: true },
  password: { type: String, required: true },
  uploads: [ {type: mongoose.Schema.Types.ObjectId, ref: "Video", default: []} ],
  purchased: [ { course: String, lecture: Number } ],
  credits: { type: Number, default: 500 }
});

UserSchema.plugin(passportLocalMongoose);

module.exports = mongoose.model('User', UserSchema);

// [{course: "CSC348", lecture: 3}, {course:"CSC301", lecture: 5}, ...]