domain = require("domain")
module.exports = ->
  (req, res, next) ->
    d = domain.create()
    d.add req
    d.add res
    d._throwErrorCount = 0

    d.on "error", (err) ->
      d._throwErrorCount++
      if (d._throwErrorCount > 1)
        return
      res.setHeader("Connection", "close")
      next err

    d.run next