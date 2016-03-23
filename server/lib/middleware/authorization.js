'use strict';

var _nodeOdata = require('node-odata');

module.exports = function (req, res, next) {
  var token = req.headers.authorization;
  if (!token) {
    return next();
  }
  return _nodeOdata.resources.user.findOne({ token: token }).exec(function (err, user) {
    if (user && !user.disabled) {
      req.user = user;
    }
    return next();
  });
};