'use strict';

var _crypto = require('crypto');

var _crypto2 = _interopRequireDefault(_crypto);

var _nodeOdata = require('node-odata');

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var router = (0, _nodeOdata.Function)();

router.post('/login', function (req, res) {
  var name = req.body.name;
  var pwd = req.body.password;
  _nodeOdata.resources.user.findOne({
    password: pwd
  }).or([{ loginName: name }, { email: name }]).exec(function (err, user) {
    if (!user) {
      return res.send(401, 'Failed to login.');
    }
    user.token = _crypto2.default.createHash('md5').update(new Date() + pwd).digest('hex');
    user.save();
    res.set('authorization', user.token);
    return res.json({
      name: user.name,
      loginName: user.loginName,
      email: user.email,
      gravatar: user.gravatar,
      disabled: user.disabled
    });
  });
});

router.post('/auto-login', function (req, res) {
  if (!req.user) {
    return res.send(401, 'Failed to auto-login.');
  }
  return res.json({
    name: req.user.name,
    loginName: req.user.loginName,
    email: req.user.email,
    gravatar: req.user.gravatar,
    disabled: req.user.disabled
  });
});

router.post('/logoff', function (req, res) {
  var token = req.get('authorization');
  _nodeOdata.resources.user.findOne({ token: token }).exec(function (err, user) {
    if (!user) {
      return res.send(400, 'User not found.');
    }
    user.token = null;
    user.save();
    return res.json({
      name: user.name,
      loginName: user.loginName,
      email: user.email,
      gravatar: user.gravatar,
      disabled: user.disabled
    });
  });
});

module.exports = router;