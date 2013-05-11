function RecentBoardViewModel() {
    var self = this;
    self.board = ko.observableArray();
    $.getJSON('/board/recent', self.board);
}