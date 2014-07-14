fs = require("fs")

module.exports = (path) ->
  modules = []
  requireAll = (path) ->
    fs.readdirSync(path).forEach (file) ->
      newPath = path + "/" + file
      stat = fs.statSync(newPath)
      if stat.isFile() and /(.*)\.(js$)/.test(file)
        modules.push(require(newPath))
      if stat.isDirectory()
        requireAll(newPath)

  requireAll(path)

  return modules