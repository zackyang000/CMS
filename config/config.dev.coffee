config = config || {}

config.host =
  domain: 'localhost'
  public: 'localhost:40000'
  admin: 'localhost:40001'
  feed: 'localhost:40002'
  img: 'localhost:40002'
  api: 'localhost:40002'

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
