fs = require("fs")

module.exports = (path) ->
  modules = []
  requireAll = (path) ->
    fs.readdirSync(path).forEach (file) ->
      newPath = path + "/" + file
      newPath = require("path").normalize newPath
      stat = fs.statSync(newPath)
      if stat.isFile() and /(.*)\.(js|coffee$)/.test(file)
        newPath = newPath.replace('.coffee', '')
        modules.push(require(newPath))
      if stat.isDirectory()
        requireAll(newPath)

  requireAll(path)

  return modules