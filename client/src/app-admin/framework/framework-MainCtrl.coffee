angular.module("framework.controller.main",[])

.controller('MainCtrl',["$scope","$rootScope","$http","$location","authorize","version", "$localStorage"
($scope,$rootScope,$http,$location,authorize, version, $localStorage) ->
  $scope.$on "loginSuccessed", ->
    version.get().then (data)->
      return if !data.length
      $scope.newVersion=data[0]
      if $scope.newVersion.ver!=$localStorage.ver
        $scope.newVersion.showDialog=true

    $scope.versionClick = ->
      $localStorage.ver=$scope.newVersion.ver
      $scope.newVersion.showDialog=false

    $rootScope.__login=true
    $rootScope.__logoff=false

    #Back to page.
    url = $scope.__returnUrl
    if url
      $scope.__returnUrl = null
      $location.path(url).replace()
    else if $location.path()=='/login'
      $location.path('/').replace()

    $scope.__themePath = '/framework/themes/'+$scope.__systemSetting.theme+'/index.html'

  $scope.$on "logoutSuccessed", ->
    $rootScope.__login=false
    $rootScope.__logoff=true


])

