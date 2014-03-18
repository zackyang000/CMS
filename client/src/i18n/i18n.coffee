angular.module("i18n",['pascalprecht.translate'])
.config(["$translateProvider",($translateProvider) ->
  $translateProvider
    .translations('en',translationsEN)
    .translations('zh',translationsZH)
])
