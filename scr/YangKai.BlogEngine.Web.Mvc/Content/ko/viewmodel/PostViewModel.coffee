class PostViewModel
  constructor: (@isadmin) ->
    self = this
    self.State = ko.observable "list"
    self.listViewModel = ko.observable(new listViewModel())
    self.detailViewModel = ko.observable(new detailViewModel())
    self.authorized = isadmin

    $.sammy(->
      @get "#!//search/:query$", ->
        query = @params["query"]
        self.listViewModel().turnpages "", "", 1, "search", query

      @get "#!/:group/:page/:type/:query/?$", ->
        channel = urlHelper.getChannelUrl()
        group = @params["group"]
        page = @params["page"]
        type = @params["type"]
        query = @params["query"]
        self.listViewModel().turnpages channel, group, page, type, query

      @get "#!/:group/:type/:query/?$", ->
        channel = urlHelper.getChannelUrl()
        group = @params["group"]
        type = @params["type"]
        query = @params["query"]
        self.listViewModel().turnpages channel, group, 1, type, query

      @get "#!/:group/:page/?$", ->
        channel = urlHelper.getChannelUrl()
        group = @params["group"]
        page = @params["page"]
        self.listViewModel().turnpages channel, group, page, "", ""

      @get "#!/:page/?$", ->
        channel = urlHelper.getChannelUrl()
        page = @params["page"]
        if IsNum(page)
          self.listViewModel().turnpages channel, "", page, "", ""
        else
          self.listViewModel().turnpages channel, page, 1, "", ""

      @get "#!/?$", ->
        channel = urlHelper.getChannelUrl()
        self.listViewModel().turnpages channel, "", 1, "", ""
    ).run()

class listViewModel
    constructor:  ->
      self = this
      self.list = ko.observableArray()
      self.loading = ko.observable(false)
      self.TotalResults = ko.observable()
      self.Pager = ko.pager(self.TotalResults)
      self.channel = ko.observable("")
      self.group = ko.observable("")
      self.type = ko.observable("")
      self.query = ko.observable("")
      self.link = ko.computed(->
        channel = self.channel()
        group = if self.group() then "/#{self.group()}" else ""
        page = if self.Pager().CurrentPage() isnt 1 then "/#{self.Pager().CurrentPage()}"  else ""
        type = if self.type() then  + "self.type()/" else ""
        query = self.query()
        "/#{channel}#!#{group}#{page}/#{type}#{query}"
      , self)
      self.request = ko.computed(->
        link = "/article?"
        link += "type=list"
        link += "&channelurl=#{self.channel()}"
        link += "&groupurl=#{self.group()}"
        link += "&id=#{self.Pager().CurrentPage()}"
        link += "&#{self.type()}=#{self.query()}" unless self.type() is ""
        link
      , self)
      self.del = (data) ->
        Messenger().run
          progressMessage: "Deleting data..."
        ,
          url: "/admin/postmanage/delete/"
          data: "id=#{data.PostId}"
          method: "post"
          dataType: "json"
          complete: (context) ->
            o = JSON.parse(context.responseText)
            if o.result
              "#{message.success}“##{data.Title}”  be moved to trash."
              data.PostStatus = "Trash"
              itemRefresh self.list, data
            else
              message.error o.reason

      self.expand = (entity) ->
        entity.IsShowDetail = not entity.IsShowDetail
        itemRefresh self.list, entity
        codeformat()

      self.Pager().CurrentPage.subscribe ->
        location.href = self.link()

      self.turnpages = (channel, group, page, type, query) ->
        $('.dropdown.open .dropdown-toggle').dropdown('toggle')
        self.channel channel
        self.group group
        self.type type
        self.query query
        self.Pager().CurrentPage page
        self.loading not self.loading()
    
        scroll 0, 0

        $.getJSON self.request(), (result) ->
          #若滑动效果未结束,则延迟绑定.
          if $("nav ul li.moving_bg").is(":animated")
            setTimeout (->self.refreshlist result), 300
          else
            self.refreshlist result

      self.refreshlist = (result) ->
        self.loading not self.loading()
        self.list result.data
        self.TotalResults result.count
        ko.utils.arrayForEach self.list(), (data) ->
          data.IsShowDetail = false

        imglazyload "#posts"

detailViewModel = ->