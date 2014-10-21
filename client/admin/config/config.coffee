config = config || {}

config.apiHost='http://localhost:30002/oData'
config.imgHost='http://localhost:30002'

#DEV
config.host =
  public: 'localhost:30000'
  admin: 'localhost:30001'
  feed: 'localhost:30002'
  img: 'localhost:30002'
  api: 'localhost:30002'

#PRD
###
config.host =
  public: 'www.woshinidezhu.com'
  admin: 'admin.woshinidezhu.com'
  feed: 'feed.woshinidezhu.com'
  img: 'img.woshinidezhu.com'
  api: 'api.woshinidezhu.com'
###

config.siteName='iShare'