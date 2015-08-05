config = config || {}

config.host =
  domain: 'zackyang.com'
  public: 'zackyang.com'
  admin: 'admin.zackyang.com'
  feed: 'feed.zackyang.com'
  img: 'img.zackyang.com'
  api: 'api.zackyang.com'

config.url =
  public: "http://#{config.host.public}"
  admin: "http://#{config.host.admin}"
  feed: "http://#{config.host.feed}"
  img: "http://#{config.host.img}"
  api: "http://#{config.host.api}"

config.site =
  name: 'Zack Yang'

config.languages =
  'English': 'en-us'
  '中文': 'zh-cn'
