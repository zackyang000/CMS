angular.module('article',
[
  'article-list'
  'article-detail'
  'article-archives'
])

.filter "i18nCategory", ['dataCacheCategories', (dataCacheCategories) ->
  (url) ->
    return ''  unless url

    for item in dataCacheCategories
      if item.url == url
        return item.name
    return ''
]
