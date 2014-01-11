angular.module('board',['resource.messages'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/board",
    templateUrl: "/Content/app/board/board.tpl.html"
    controller: 'BoardCtrl')
])

.controller('BoardCtrl',
["$scope","$translate","progressbar","Message","account" 
($scope,$translate,progressbar,Message,account) ->
  $scope.$parent.title='Message Boards'
  $scope.$parent.showBanner=false

  account.get().then (data) ->
    $scope.entity=
      Author:data.UserName
      Email:data.Email
      Url:data.Url
    $scope.editmode=!data.UserName

  $scope.loading=$translate("global.loading")
  $scope.list = Message.query $filter:'IsDeleted eq false',->
    for item in $scope.list.value
      if !item.Avatar
        if item.Email
          item.Avatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
        else
          item.Avatar='/Content/img/avatar.png'
    $scope.loading=""

  $scope.save = () ->
    $scope.submitted=true
    if $scope.form.$invalid
      return

    progressbar.start()
    $scope.loading = $translate("global.post")
    $scope.entity.BoardId=UUID.generate()
    Message.save $scope.entity
    ,(data)->
      message.success $translate("board.complete")
      $scope.list.value.unshift(data)
      $scope.entity.Content=""
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""
    ,(error)->
      $scope.submitted=false
      $scope.loading = ""

  $scope.remove = (item) ->
    message.confirm ->
      Message.remove id:"(guid'#{item.BoardId}')",->
        item.IsDeleted=true
        message.success "Message has been removed."
])