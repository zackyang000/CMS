angular.module("zy.services.messager", ['resource.users'])

.factory "messager", ->

  success : (msg) ->
    swal "complate!", msg, "success"

  error : (msg) ->
    msg = 'Unauthorized.' if (msg == '401')
    swal "complate!", msg, "error"

  confirm : (callback, msg) ->
    swal
      title: "Do you want to continue?"
      text: ''
      type: "warning"
      showCancelButton: true
      confirmButtonColor: "#DD6B55"
      confirmButtonText: "Yes !"
      closeOnConfirm: false
    , ->
      callback() if callback