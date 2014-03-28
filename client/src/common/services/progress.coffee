angular.module("zy.services.progress", ['resource.users']).provider "progress", ->
  @$get = ["$document", "$window", "$compile", "$rootScope"
    ($document, $window, $compile, $rootScope) ->
      $scope = $rootScope
      $body = $document.find("body")
      el = $compile("<div class='loadingbox' ng-show='loading'>{{ loading }}</div>")($scope)
      $body.append el

      (
        start : (tip) ->
          $scope.loading = (tip || "Loading") + "..."
        complete : ->
          $scope.loading = ""
        status : ->
          !!$scope.loading
      )
  ]
  return

