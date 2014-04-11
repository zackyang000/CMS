fs = require("fs")
appPath = process.cwd()

module.exports = ->
  bootstrapModels = ->
    walk = (path) ->
      fs.readdirSync(path).forEach (file) ->
        newPath = path + "/" + file
        stat = fs.statSync(newPath)
        if stat.isFile()
          require newPath if /(.*)\.(js$)/.test(file)
        else
          walk newPath if stat.isDirectory()
    walk(appPath + "/models")

  bootstrapModels()
