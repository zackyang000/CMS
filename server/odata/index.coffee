requires = require('./../utils/requires')

module.exports =
  setup : (server) ->
    dirs = ['article', 'board', 'gallery', 'system']
    for dir in dirs
      for item in requires("#{__dirname}/#{dir}") when item.url
        server.resources.register item

    require('./functions')(server)
