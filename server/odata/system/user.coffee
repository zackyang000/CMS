odata = require('node-odata')
auth = require('../auth')
model = require('../../models')
repository = require('../../repositories')

rest = {}

rest.get =
  auth: auth.admin

rest.getAll =
  auth: auth.admin

rest.post =
  auth: auth.admin

rest.put =
  auth: auth.admin

rest.delete =
  auth: auth.admin


actions = {}

# 添加评论 todo: 改为user put after
actions['/update-username'] = (req, res, next) ->
  oldName = req.body.oldName
  newName = req.body.newName
  repository.get('article').find( {'meta.author': oldName }).exec (err, articles) ->
    for article in articles
      article.meta.author = newName
      article.save()
    res.send(202, "processing...")
actions['/update-username'].auth = auth.admin


odata.resources.register
  url: '/users'
  model: model.user
  options:
    defaultOrderby: 'date desc'
  rest: rest
  actions: actions
