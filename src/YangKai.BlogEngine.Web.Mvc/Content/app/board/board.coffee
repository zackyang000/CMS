angular.module('board',['MessageServices'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/board",
    templateUrl: "/Content/app/board/board.tpl.html"
    controller: 'BoardCtrl')
])

.controller('BoardCtrl',
["$scope","$translate","progressbar","Message", 
($scope,$translate,progressbar,Message) ->
  $scope.$parent.title='Message Boards'
  $scope.$parent.showBanner=false
  $scope.entity= {}

  $scope.$watch 'User',->
    if $scope.User
      $scope.entity.Author = $scope.User.UserName
      $scope.entity.Email = $scope.User.Email
      $scope.entity.Url = $scope.User.Url
      $scope.AuthorForDisplay=$scope.User.UserName
      $scope.editmode=$scope.User.UserName=='' or not $scope.User.UserName?
  
  $scope.loading=$translate("global.loading")
  $scope.list = Message.query $filter:'IsDeleted eq false',->
    for item in $scope.list.value
      if item.Email
        item.Gravatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
      else
        item.Gravatar='/Content/img/avatar.png'
    $scope.loading=""

  $scope.save = () ->
    progressbar.start()
    $scope.loading = $translate("global.post")
    $scope.entity.BoardId=UUID.generate()
    Message.save $scope.entity
    ,(data)->
      message.success $translate("board.complete")
      $scope.list.value.unshift(data)
      $scope.entity.Content=""
      $scope.AuthorForDisplay=data.Author
      $scope.editmode=false
      angular.resetForm($scope, 'form')
      progressbar.complete()
      $scope.loading = ""

  $scope.remove = (item) ->
    message.confirm ->
      Message.remove id:"(guid'#{item.BoardId}')",->
        item.IsDeleted=true
        message.success "Message has been removed."
])