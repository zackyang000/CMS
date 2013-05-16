class RecentCommentsViewModel
  constructor: (@channelUrl) ->
    @list = ko.observableArray()
    $.getJSON "/comment/recent?channelurl=#{@channelUrl}", @list