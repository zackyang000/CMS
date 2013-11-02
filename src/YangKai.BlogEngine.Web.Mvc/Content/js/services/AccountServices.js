
angular.module("AccountServices", []).factory("account", [
  '$http', '$rootScope', function($http, $rootScope) {
    return $http.get("/admin/getuser").success(function(data) {
      if (data.Email) {
        data.Gravatar = 'http://www.gravatar.com/avatar/' + md5(data.Email);
      } else {
        data.Gravatar = '/Content/img/avatar.png';
      }
      return $rootScope.User = data;
    });
  }
]);
