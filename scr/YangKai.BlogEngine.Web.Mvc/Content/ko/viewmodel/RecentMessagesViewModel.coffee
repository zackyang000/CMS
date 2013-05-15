class RecentMessagesViewModel
  constructor: ->
    @list = ko.observableArray()
    $.getJSON "/board/recent", @list