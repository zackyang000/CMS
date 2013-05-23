function BoardViewModel(isAdmin) {
    var self = this;

    self.list = ko.observableArray();
    self.username = ko.observable('');
    self.email = ko.observable('');
    self.url = ko.observable('');
    self.loading = ko.observable("");
    self.error = ko.observable("");
    self.controldisplayauthoreditor = ko.observable(false);
    self.isAdmin = ko.observable(isAdmin);
            
    self.add = function (formElement) {
        self.error("");
        self.loading("正在提交");
        $.post("/board/add", $(formElement).serialize(), null, "json")
            .done(function (o) {
                self.loading("");
                if (o.result) {
                    $("#Content").val("");
                    self.list.unshift(o.model);
                } else {
                    self.error(o.reason);
                }
            });
    };

    self.del = function(entity) {
        $.post("/board/delete", "id=" + entity.BoardId, null, "json")
            .done(function(o) {
                if (o.result) {
                    message.success('“#' + entity.Content + '”  be moved to trash.');
                    entity.IsDeleted = true;
                    itemRefresh(self.list, entity);
                } else {
                    message.error(o.reason);
                }
            });
    };

    self.renew = function (entity) {
        $.post("/board/renew", "id=" + entity.BoardId, null, "json")
            .done(function (o) {
                if (o.result) {
                    message.success('“#' + entity.Content + '”  be renew.');
                    entity.IsDeleted = false;
                    itemRefresh(self.list, entity);
                } else {
                    message.error(o.reason);
                }
            });
    };
            
    self.change = function() {
        self.controldisplayauthoreditor(!self.controldisplayauthoreditor());
    };

    $.getJSON('/board/list', self.list);
    $.getJSON('/comment/UserInfo', function (data) {
        self.username(data.Author);
        self.email(data.Email);
        self.url(data.Url);
    });
}