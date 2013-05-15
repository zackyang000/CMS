var RecentMessagesViewModel;

RecentMessagesViewModel = (function() {

  function RecentMessagesViewModel() {
    this.list = ko.observableArray();
    $.getJSON("/board/recent", this.list);
  }

  return RecentMessagesViewModel;

})();
