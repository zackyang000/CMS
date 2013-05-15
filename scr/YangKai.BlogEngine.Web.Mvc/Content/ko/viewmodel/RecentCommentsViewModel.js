var RecentCommentsViewModel;

RecentCommentsViewModel = (function() {

  function RecentCommentsViewModel(channelUrl, groupUrl) {
    this.channelUrl = channelUrl;
    this.groupUrl = groupUrl;
    this.list = ko.observableArray();
    $.getJSON("/comment/recent?channelurl=" + this.channelUrl + "&groupurl=" + this.groupUrl, this.list);
  }

  return RecentCommentsViewModel;

})();
