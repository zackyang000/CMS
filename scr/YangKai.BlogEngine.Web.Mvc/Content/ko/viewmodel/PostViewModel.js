var PostViewModel, detailViewModel, listViewModel;

PostViewModel = (function() {

  function PostViewModel(isadmin) {
    var self;
    this.isadmin = isadmin;
    self = this;
    self.State = ko.observable("list");
    self.listViewModel = ko.observable(new listViewModel());
    self.detailViewModel = ko.observable(new detailViewModel());
    self.authorized = isadmin;
    $.sammy(function() {
      this.get("#!//search/:query$", function() {
        var query;
        query = this.params["query"];
        return self.listViewModel().turnpages("", "", 1, "search", query);
      });
      this.get("#!/:group/:page/:type/:query/?$", function() {
        var channel, group, page, query, type;
        channel = urlHelper.getChannelUrl();
        group = this.params["group"];
        page = this.params["page"];
        type = this.params["type"];
        query = this.params["query"];
        return self.listViewModel().turnpages(channel, group, page, type, query);
      });
      this.get("#!/:group/:type/:query/?$", function() {
        var channel, group, query, type;
        channel = urlHelper.getChannelUrl();
        group = this.params["group"];
        type = this.params["type"];
        query = this.params["query"];
        return self.listViewModel().turnpages(channel, group, 1, type, query);
      });
      this.get("#!/:group/:page/?$", function() {
        var channel, group, page;
        channel = urlHelper.getChannelUrl();
        group = this.params["group"];
        page = this.params["page"];
        return self.listViewModel().turnpages(channel, group, page, "", "");
      });
      this.get("#!/:page/?$", function() {
        var channel, page;
        channel = urlHelper.getChannelUrl();
        page = this.params["page"];
        if (IsNum(page)) {
          return self.listViewModel().turnpages(channel, "", page, "", "");
        } else {
          return self.listViewModel().turnpages(channel, page, 1, "", "");
        }
      });
      return this.get("#!/?$", function() {
        var channel;
        channel = urlHelper.getChannelUrl();
        return self.listViewModel().turnpages(channel, "", 1, "", "");
      });
    }).run();
  }

  return PostViewModel;

})();

listViewModel = (function() {

  function listViewModel() {
    var self;
    self = this;
    self.list = ko.observableArray();
    self.loading = ko.observable(false);
    self.TotalResults = ko.observable();
    self.Pager = ko.pager(self.TotalResults);
    self.channel = ko.observable("");
    self.group = ko.observable("");
    self.type = ko.observable("");
    self.query = ko.observable("");
    self.link = ko.computed(function() {
      var channel, group, page, query, type;
      channel = self.channel();
      group = self.group() ? "/" + (self.group()) : "";
      page = self.Pager().CurrentPage() !== 1 ? "/" + (self.Pager().CurrentPage()) : "";
      type = self.type() ? +"self.type()/" : "";
      query = self.query();
      return "/" + channel + "#!" + group + page + "/" + type + query;
    }, self);
    self.request = ko.computed(function() {
      var link;
      link = "/article?";
      link += "type=list";
      link += "&channelurl=" + (self.channel());
      link += "&groupurl=" + (self.group());
      link += "&id=" + (self.Pager().CurrentPage());
      if (self.type() !== "") link += "&" + (self.type()) + "=" + (self.query());
      return link;
    }, self);
    self.del = function(data) {
      return Messenger().run({
        progressMessage: "Deleting data..."
      }, {
        url: "/admin/postmanage/delete/",
        data: "id=" + data.PostId,
        method: "post",
        dataType: "json",
        complete: function(context) {
          var o;
          o = JSON.parse(context.responseText);
          if (o.result) {
            "" + message.success + "“#" + data.Title + "”  be moved to trash.";
            data.PostStatus = "Trash";
            return itemRefresh(self.list, data);
          } else {
            return message.error(o.reason);
          }
        }
      });
    };
    self.expand = function(entity) {
      entity.IsShowDetail = !entity.IsShowDetail;
      itemRefresh(self.list, entity);
      return codeformat();
    };
    self.Pager().CurrentPage.subscribe(function() {
      return location.href = self.link();
    });
    self.turnpages = function(channel, group, page, type, query) {
      $('.dropdown.open .dropdown-toggle').dropdown('toggle');
      self.channel(channel);
      self.group(group);
      self.type(type);
      self.query(query);
      self.Pager().CurrentPage(page);
      self.loading(!self.loading());
      scroll(0, 0);
      return $.getJSON(self.request(), function(result) {
        if ($("nav ul li.moving_bg").is(":animated")) {
          return setTimeout((function() {
            return self.refreshlist(result);
          }), 300);
        } else {
          return self.refreshlist(result);
        }
      });
    };
    self.refreshlist = function(result) {
      self.loading(!self.loading());
      self.list(result.data);
      self.TotalResults(result.count);
      ko.utils.arrayForEach(self.list(), function(data) {
        return data.IsShowDetail = false;
      });
      return imglazyload("#posts");
    };
  }

  return listViewModel;

})();

detailViewModel = function() {};
