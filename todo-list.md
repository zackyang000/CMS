#### public

- 利用artile字段 status, password
- 根据tags查询文章列表, 使用tag实体
- 编辑category的时候应该把article所属category一并修改(需要调整node-data, 因为after中无法获取修改前的数据)
- 最近浏览功能
- hidden全改为disable, 所有集合增加disable/enable
- 增加rainbow
- 后台生成gravatar
- 相册显示照片数量

#### admin

- 相册删除功能
- user修改密码
- user删除
- 修改登陆token记录方式, 改为允许多个 token 存在.

#### code
- 调整init data
- config 改为 $config
- 调整 odata 代码
- server 端代码不需 _dist
