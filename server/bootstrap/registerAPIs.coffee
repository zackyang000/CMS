fs = require("fs")
appPath = process.cwd()

module.exports = (app, prefix) ->
  bootstrapModels = ->
    walk = (path) ->
      fs.readdirSync(path).forEach (file) ->
        newPath = path + "/" + file
        stat = fs.statSync(newPath)
        if stat.isFile()
          require(newPath)(app, prefix) if /(.*)\.(js$)/.test(file)
        else
          walk newPath if stat.isDirectory()
    walk(appPath + "/routes")

  bootstrapModels()
