var RecentCommentsViewModel;

RecentCommentsViewModel = (function() {

  function RecentCommentsViewModel(channelUrl) {
    this.channelUrl = channelUrl;
    this.list = ko.observableArray();
    $.getJSON("/comment/recent?channelurl=" + this.channelUrl, this.list);
  }

  return RecentCommentsViewModel;

})();
