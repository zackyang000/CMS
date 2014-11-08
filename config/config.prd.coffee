config = config || {}

config.host =
  domain: 'woshinidezhu.com'
  public: 'www.woshinidezhu.com'
  admin: 'admin.woshinidezhu.com'
  feed: 'feed.woshinidezhu.com'
  img: 'img.woshinidezhu.com'
  api: 'api.woshinidezhu.com'

config.url =
  public: "http://#{config.host.public}"
  admin: "http://#{config.host.admin}"
  feed: "http://#{config.host.feed}"
  img: "http://#{config.host.img}"
  api: "http://#{config.host.api}/oData"

config.site =
  name: 'iShare'

config.languages =
  'English': 'en-us'
  '中文': 'zh-cn'
