angular.module("zy.services.tip", []).provider "tip", ->
  @$get = ["$document", "$window", "$compile", "$rootScope"
    ($document, $window, $compile, $rootScope) ->
      $scope = $rootScope

      (
        show : (tip) ->
          $scope.loadingTip = (tip || "Loading") + "..."
        hide : ->
          $scope.loadingTip = ""
        status : ->
          !!$scope.loadingTip
      )
  ]
  return

