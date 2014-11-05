angular.module("zy.untils.promissLoading", [])

.config ($provide) ->
  $provide.decorator "$q", ["$delegate", "$rootScope"
    ($delegate, $rootScope) ->
      pendingPromisses = 0
      $rootScope.$watch ->
        pendingPromisses > 0
      , (loading) ->
        $rootScope.loading = loading

      $q = $delegate
      origDefer = $q.defer
      $q.defer = ->
        defer = origDefer()
        pendingPromisses++
        defer.promise.finally -> pendingPromisses--
        return defer
      return $q
  ]