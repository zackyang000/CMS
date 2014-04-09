users = require('../controllers/users')

module.exports = (app) ->
  app.get('/users', users.all)
  app.post('/users', users.create)
  app.get('/users/:userId', users.get)
  app.put('/users/:userId', users.update)
  app.del('/users/:userId', users.destroy)
  app.param('userId', users.user)