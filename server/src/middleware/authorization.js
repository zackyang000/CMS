import { resources } from 'node-odata';

module.exports = (req, res, next) => {
  const token = req.headers.authorization;
  if (!token) {
    return next();
  }
  return resources.user.findOne({ token }).exec((err, user) => {
    if (user && !user.disabled) {
      req.user = user;
    }
    return next();
  });
};
