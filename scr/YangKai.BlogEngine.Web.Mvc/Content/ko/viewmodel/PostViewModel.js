var PostViewModel = function (isadmin) {
    var self = this;
    self.State = ko.observable('list');
    self.listViewModel = ko.observable(new listViewModel());
    self.detailViewModel = ko.observable(new detailViewModel());
    self.authorized = isadmin;

    $.sammy(function () {

        this.get("#!//search/:query$", function () {
            var query = this.params['query'];
            self.listViewModel().turnpages('', '', 1, 'search', query);
        });

        this.get("#!/:group/:page/:type/:query/?$", function () {
            var channel = urlHelper.getChannelUrl();
            var group = this.params['group'];
            var page = this.params['page'];
            var type = this.params['type'];
            var query = this.params['query'];
            self.listViewModel().turnpages(channel, group, page, type, query);
        });

        this.get("#!/:group/:type/:query/?$", function () {
            var channel = urlHelper.getChannelUrl();
            var group = this.params['group'];
            var type = this.params['type'];
            var query = this.params['query'];
            self.listViewModel().turnpages(channel, group, 1, type, query);
        });

        this.get("#!/:group/:page/?$", function () {
            var channel = urlHelper.getChannelUrl();
            var group = this.params['group'];
            var page = this.params['page'];
            self.listViewModel().turnpages(channel, group, page, '', '');
        });

        this.get("#!/:page/?$", function () {
            var channel = urlHelper.getChannelUrl();
            var page = this.params['page'];
            if (IsNum(page)) {
                self.listViewModel().turnpages(channel, '', page, '', '');
            } else {
                self.listViewModel().turnpages(channel, page, 1, '', '');
            }
        });

        this.get("#!/?$", function () {
            var channel = urlHelper.getChannelUrl();
            self.listViewModel().turnpages(channel, '', 1, '', '');
        });

    }).run();
};

var listViewModel = function () {
    var self = this;

    self.list = ko.observableArray();
    self.loading = ko.observable(false);

    self.TotalResults = ko.observable();
    self.Pager = ko.pager(self.TotalResults);
    self.channel = ko.observable('');
    self.group = ko.observable('');
    self.type = ko.observable('');
    self.query = ko.observable('');

    self.link = ko.computed(function () {
        var channel = self.channel();
        var group = self.group() ? '/' + self.group() : '';
        var page = self.Pager().CurrentPage() != 1 ? '/' + self.Pager().CurrentPage() : '';
        var type = self.type() ? self.type() + '/' : '';
        var query = self.query();
        return "/" + channel + "#!" + group + page + "/" + type + query;
    }, self);
    
    self.request = ko.computed(function () {
        var link = '/article?';
        link += 'type=list';
        link += '&channelurl=' + self.channel();
        link += '&groupurl=' + self.group();
        link += '&id=' + self.Pager().CurrentPage();
        if (self.type() != '') {
            link += '&' + self.type() + '=' + self.query();
        }
        return link;
    }, self);

    self.del = function (data) {
        Messenger().run(
            { progressMessage: 'Deleting data...' },
            {
                url: "/admin/postmanage/delete/",
                data: "id=" + data.PostId,
                method: "post",
                dataType: "json",
                complete: function (context) {
                    var o = JSON.parse(context.responseText);
                    if (o.result) {
                        message.success('“#' + data.Title + '”  be moved to trash.');
                        data.PostStatus = 'Trash';
                        itemRefresh(self.list, data);
                    } else {
                        message.error(o.reason);
                    }
                }
            }
        );
    };

    self.expand = function (entity) {
        entity.IsShowDetail = !entity.IsShowDetail;
        itemRefresh(self.list, entity);
        codeformat();
    };
    
    self.Pager().CurrentPage.subscribe(function () {
        location.href = self.link();
    });

    self.turnpages = function (channel, group, page, type, query) {
        self.channel(channel);
        self.group(group);
        self.type(type);
        self.query(query);
        self.Pager().CurrentPage(page);

        self.loading(!self.loading());

        //定位到顶部
        scroll(0, 0);

        $.getJSON(self.request(), function (result) {
            //若滑动效果未结束,则延迟绑定.
            if ($("nav ul li.moving_bg").is(":animated")) {
                setTimeout(function () { self.refreshlist(result); }, 300);
            } else {
                self.refreshlist(result);
            }
        });
    };

    self.refreshlist = function (result) {
        self.loading(!self.loading());
        self.list(result.data);
        self.TotalResults(result.count);
        ko.utils.arrayForEach(self.list(), function (data) { data.IsShowDetail = false; });
        imglazyload("#posts");
    };
};

var detailViewModel = function () {
};