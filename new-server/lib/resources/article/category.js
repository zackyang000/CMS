'use strict';

var _nodeOdata = require('node-odata');

var _models = require('../../models');

var _models2 = _interopRequireDefault(_models);

var _auth = require('../auth');

var _auth2 = _interopRequireDefault(_auth);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

module.exports = (0, _nodeOdata.Resource)('category', _models2.default.category).orderBy('date desc').post().auth(_auth2.default.admin).put().auth(_auth2.default.admin).delete().auth(_auth2.default.admin);