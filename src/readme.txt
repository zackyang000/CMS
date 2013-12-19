CMS
==========

DEMO: http://www.woshinidezhu.com

简介

本系统主要用于搭建博客系统。

前端基于AngularJS构建。

服务端基于ASP.NET WEB API编写，采用微软OData协议的RESTful API与前端进行通信。基于Entity Framework的持久化方案。

开发环境

Visual Studio 2012 或以上

开始

1.使用Visual Studio打开解决方案。

2.使用Nuget获取丢失的Packages。

2.修改Mvc项目下web.config文件中数据库链接字符串。(采用SQL Server 2005或以上)

3.编译运行,程序将自动创建数据库并插入测试数据。

License

MIT.

TODO:
统一使用PUT来新增和保存数据