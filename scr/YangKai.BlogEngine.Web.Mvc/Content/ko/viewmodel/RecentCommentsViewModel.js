function RecentCommentsViewModel(channelUrl, groupUrl) {
    var self = this;
    self.comments = ko.observableArray();
    self.groupUrl = groupUrl;
    $.getJSON('/comment/recent?channelurl=' + channelUrl + '&groupurl=' + groupUrl, self.comments);
}