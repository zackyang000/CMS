"use strict";

module.exports = {
  admin: function admin(req) {
    return !!req.user;
  }
};