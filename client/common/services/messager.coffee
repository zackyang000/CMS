angular.module("zy.services.messager", ['resource.users'])

.factory "messager", ->
  Messenger.options =
    extraClasses: 'messenger-fixed messenger-on-top'
    theme: 'flat'

  success : (msg) ->
    Messenger().post({ message : msg, type : 'success', showCloseButton : true })

  error : (msg) ->
    msg = 'Unauthorized.' if (msg == '401')
    Messenger().post
      message : msg
      type : 'error'
      showCloseButton : true
      delay : 600

  confirm : (callback,msg) ->
    Messenger.options =
      extraClasses: 'messenger-fixed messenger-on-top'
      theme: 'flat'
    msg = Messenger().post
      message : msg || "Do you want to continue?"
      id : "Only-one-message"
      showCloseButton : true
      actions :
        OK :
          label : "OK"
          phrase : "Confirm"
          delay : 60
          action : ->
            callback()
            msg.cancel()
        cancel :
          action : ->
            msg.cancel()
