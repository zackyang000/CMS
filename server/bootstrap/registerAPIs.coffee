fs = require("fs")
appPath = process.cwd()

module.exports = (app) ->
  bootstrapModels = ->
    walk = (path) ->
      fs.readdirSync(path).forEach (file) ->
        newPath = path + "/" + file
        stat = fs.statSync(newPath)
        if stat.isFile()
          require(newPath)(app) if /(.*)\.(js$)/.test(file)
        else
          walk newPath if stat.isDirectory()
    walk(appPath + "/routes")

  bootstrapModels()
