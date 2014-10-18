cluster = require("cluster")
#logger = require("./common/logger")
numCPUs = require("os").cpus().length
if cluster.isMaster
  # Fork workers.
  i = 0
  while i < numCPUs
    cluster.fork()
    console.log("start " + i)
    i++

  cluster.on "exit", (worker, code, signal) ->
    console.error "worker #{worker.process.pid} died."
    cluster.fork()
else
    require "./server"