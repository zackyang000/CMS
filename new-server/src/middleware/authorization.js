import { resources } from 'node-odata';

module.exports = (req, res, next) => {
  token = req.headers.authorization;
  if (!token) {
    return next();
  }
  resources.user.findOne({token: token}).exec((err, user) => {j
    if(user && !user.disabled) {
      req.user = user;
    }
    return next();
  });
};
