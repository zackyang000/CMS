angular.module('issue',['IssueServices'])

.filter 'filterByDate', ->
  (input,filterDate) ->
    return input if !input
    list=[]
    for item in input
      if filterDate==''
        list.push item
      if filterDate=='day' && moment(new Date()).format('YYYY-MM-DD')==moment(item.CreateDate).format('YYYY-MM-DD')
        list.push item
      if filterDate=='week' && moment(new Date()).diff(item.CreateDate, 'days')<=7
        list.push item
      if filterDate=='month' && moment(new Date()).diff(item.CreateDate, 'days')<=30
        list.push item
    return list

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
        item.date=moment(item.CreateDate).fromNow()

      $scope.list=data.value
      $scope.setGroup()

  $scope.setGroup = ->
      statu={}
      project={}
      date={}

      $scope.count=
        statu:0
        project:0
        date:0

      statu['Open']=0
      statu['Closed']=0
      statu['Cancel']=0
      for item in $scope.list
        if $scope.filter.Project is '' || item.Project is $scope.filter.Project
          if $scope.filterDate==''
            unless statu[item.Statu]
              statu[item.Statu]=0
            statu[item.Statu]++
            $scope.count.statu++
          if $scope.filterDate=='day' && moment(new Date()).format('YYYY-MM-DD')==moment(item.CreateDate).format('YYYY-MM-DD')
            unless statu[item.Statu]
              statu[item.Statu]=0
            statu[item.Statu]++
            $scope.count.statu++
          if $scope.filterDate=='week' && moment(new Date()).diff(item.CreateDate, 'days')<=7
            unless statu[item.Statu]
              statu[item.Statu]=0
            statu[item.Statu]++
            $scope.count.statu++
          if $scope.filterDate=='month' && moment(new Date()).diff(item.CreateDate, 'days')<=30
            unless statu[item.Statu]
              statu[item.Statu]=0
            statu[item.Statu]++
            $scope.count.statu++


      for item in $scope.list
        if $scope.filter.Statu is '' || item.Statu is $scope.filter.Statu
          if $scope.filterDate==''
            unless project[item.Project]
              project[item.Project]=0
            project[item.Project]++
            $scope.count.project++
          if $scope.filterDate=='day' && moment(new Date()).format('YYYY-MM-DD')==moment(item.CreateDate).format('YYYY-MM-DD')
            unless project[item.Project]
              project[item.Project]=0
            project[item.Project]++
            $scope.count.project++
          if $scope.filterDate=='week' && moment(new Date()).diff(item.CreateDate, 'days')<=7
            unless project[item.Project]
              project[item.Project]=0
            project[item.Project]++
            $scope.count.project++
          if $scope.filterDate=='month' && moment(new Date()).diff(item.CreateDate, 'days')<=30
            unless project[item.Project]
              project[item.Project]=0
            project[item.Project]++
            $scope.count.project++
      
      date['day']=0
      date['week']=0
      date['month']=0
      for item in $scope.list
        if $scope.filter.Statu is '' || item.Statu is $scope.filter.Statu
          if $scope.filter.Project is '' || item.Project is $scope.filter.Project
            if moment(item.CreateDate).format('YYYY-MM-DD')==moment(new Date()).format('YYYY-MM-DD')
              date['day']++
            if moment(new Date()).diff(item.CreateDate, 'days')<=7
              date['week']++
            if moment(new Date()).diff(item.CreateDate, 'days')<=30
              date['month']++
            $scope.count.date++

      $scope.statu=statu
      $scope.date=date

      $scope.project=[]
      for key,value of project
        if project.hasOwnProperty key
          $scope.project.push
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

  $scope.edit = (item)->
    item.show=true
    item.edit=!item.edit
    item.title=item.Title
    item.content=item.Content
    item.result=item.Result
    item.statu=item.Statu

  $scope.handle = (item)->
    debugger
    item.Title = item.title
    item.Content = item.content
    item.Result = item.result
    item.Statu = item.statu
    $scope.loading = 'save'
    entity=angular.copy item
    delete entity.edit
    delete entity.show
    delete entity.date
    delete entity.title
    delete entity.content
    delete entity.result
    delete entity.statu
    Issue.update {id:"(guid'#{entity.IssueId}')"},entity,(data)->
      message.success "Save issue successfully."
      $scope.editDialog = false
      $scope.loading=""
      $scope.get()
  $scope.get()
])