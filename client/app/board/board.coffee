angular.module('board',['resource.board'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/board",
      templateUrl: "/app/board/board.tpl.html"
      controller: 'BoardCtrl'
      title: 'Message Boards'
      resolve:
        messages: ['$q','Board',($q, Board)->
          deferred = $q.defer()
          Board.query
            $top: 10000
          ,(data) ->
            deferred.resolve data.value
          deferred.promise
        ]
])

.controller('BoardCtrl',
["$scope", "$translate", "messages", "context", "ngProgress", "Board", "messager"
($scope, $translate, messages, context, ngProgress, Board, messager) ->

  $scope.messages = messages

  #初始化新评论
  $scope.entity =
    author :
      name : context.account.name
      email : context.account.email
  $scope.editmode = !context.account.name
  $scope.isAdmin = context.auth.admin

  #提交评论
  $scope.save = ->
    $scope.submitted = true
    return  if $scope.form.$invalid
    ngProgress.start()
    $scope.loading = $translate("global.post")
    Board.save $scope.entity
    , (data)->
      $scope.messages.push(data)
      $scope.entity.content = ""
      ngProgress.complete()
      $scope.submitted = false
      $scope.loading = ""
      context.account =
        name: $scope.entity.author.name
        email: $scope.entity.author.email
        url: $scope.entity.url
    ,(error)->
      ngProgress.complete()
      $scope.submitted = false
      $scope.loading = ""

  $scope.remove = (item, index) ->
    messager.confirm ->
      Board.remove id: item._id, ->
        $scope.messages.splice(index, 1)
        messager.success "Message has been removed."
])