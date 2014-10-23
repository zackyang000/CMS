angular.module("zy.services.messager", [])

.factory "messager", ->
  success : (msg) ->
    alert(msg)

  error : (msg) ->
    alert(msg)

  confirm : (callback,msg) ->
    if confirm(msg || "Do you want to continue?")
      callback()
