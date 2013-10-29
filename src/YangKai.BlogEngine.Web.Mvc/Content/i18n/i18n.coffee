angular.module("l18n",['pascalprecht.translate'])
.config(["$translateProvider",($translateProvider) ->
  $translateProvider
    .translations('en',translationsEN)
    .translations('zh',translationsZH)
])
