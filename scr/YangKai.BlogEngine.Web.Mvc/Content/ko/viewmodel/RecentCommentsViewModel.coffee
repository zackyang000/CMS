class RecentCommentsViewModel
  constructor: (@channelUrl;@groupUrl) ->
    @list = ko.observableArray()
    $.getJSON "/comment/recent?channelurl=#{@channelUrl}&groupurl=#{@groupUrl}", @list