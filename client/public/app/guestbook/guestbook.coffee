angular.module('board',['resource.board'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/guestbook",
      templateUrl: "/app/guestbook/guestbook.tpl.html"
      controller: 'GuestbookCtrl'
      title: 'Guestbook'
      resolve:
        messages: ['$q','Board',($q, Board)->
          deferred = $q.defer()
          Board.list
            $top: 10000
          ,(data) ->
            deferred.resolve data.value
          deferred.promise
        ]
])

.controller('GuestbookCtrl',
["$scope", "$translate", "messages", "context", "ngProgress", "Board", "messager"
($scope, $translate, messages, context, ngProgress, Board, messager) ->

  $scope.messages = messages

  #初始化新评论
  $scope.entity =
    author :
      name : context.account.name
      email : context.account.email
  $scope.editmode = !context.account.name
  $scope.auth = context.auth

  #提交评论
  $scope.save = ->
    $scope.submitted = true
    return  if $scope.form.$invalid
    ngProgress.start()
    $scope.loading = $translate("global.post")
    $scope.entity.date = new Date()
    Board.post $scope.entity
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
