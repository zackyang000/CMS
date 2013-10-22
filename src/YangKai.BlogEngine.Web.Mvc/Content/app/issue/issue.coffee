angular.module('issue',['IssueServices'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/issue",
    templateUrl: "/content/app/issue/issue.tpl.html"
    controller: 'IssueCtrl')
])

.controller('IssueCtrl',
["$scope","$routeParams","$location","Issue"
($scope,$routeParams,$location,Issue) ->
  $scope.$parent.title='Issues'
  $scope.$parent.showBanner=false

  $scope.projects=['API Portal'
    'API Framework'
    'ServiceStack.Text'
    'Framework API/ API SDK'
    'Oversea WCF Framework'
    'Framework Tools'
    'Auth Service'
    'Gateway'
    'Oversea Data Access'
    'Cassandra Adapter'
    'Document Tool'
    'Common API'
    'API Notify'
    'Newegg Central Framework'
    'HR Tools'].sort()

  $scope.get = ->
    Issue.query (data)->
      for item in data.value
        item.CreateDate=moment(item.CreateDate).fromNow()

      $scope.list=data.value
      
      statu={}
      project={}
      user={}

      for item in $scope.list
        unless statu[item.Statu]
          statu[item.Statu]=0
        statu[item.Statu]++
        unless project[item.Project]
          project[item.Project]=0
        project[item.Project]++
        unless user[item.Author]
          user[item.Author]=0
        user[item.Author]++
        
      $scope.statu=[]
      for key,value of statu
        if statu.hasOwnProperty key
          $scope.statu.push
            key:key
            value:value
      $scope.project=[]
      for key,value of project
        if project.hasOwnProperty key
          $scope.project.push
            key:key
            value:value
      $scope.user=[]
      for key,value of user
        if user.hasOwnProperty key
          $scope.user.push
            key:key
            value:value

  $scope.add = ()->
    $scope.entity = {}
    $scope.editDialog = true

  $scope.save = ->
    $scope.loading = 'save'
    entity = $scope.entity
    entity.IssueId=UUID.generate()
    entity.Statu='Open'
    Issue.update {id:"(guid'#{entity.IssueId}')"},entity,(data)->
      message.success "Create issue successfully."
      $scope.editDialog = false
      $scope.loading=""
      $scope.get()

  $scope.handle = ->

  $scope.get()
])