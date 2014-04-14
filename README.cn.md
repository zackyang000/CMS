***
[ENGLISH README](https://github.com/TossShinHwa/CMS/blob/master/README.md)
***

Blog Engine
===========
[![Build Status](https://api.travis-ci.org/TossShinHwa/CMS.png)](https://api.travis-ci.org/TossShinHwa/CMS)
[![Dependency Status](https://david-dm.org/ChrisWren/grunt-nodemon.png)](https://david-dm.org/TossShinHwa/CMS)
[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/TossShinHwa/cms/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

查看在线演示? 请访问 http://www.woshinidezhu.com
***

## 目的

为了展示如何使用AngularJS编写一个基于REST通信的**前后端分离**实际项目。

## 使用的技术

* 酷炫的 [AngularJS](http://www.angularjs.org/)
* CSS使用 [Twitter Bootstrap](http://getbootstrap.com/)
* REST API基于 [ASP.NET WEB API](http://www.asp.net/web-api/) 构建
* 使用 [Entity Framework](http://msdn.microsoft.com/en-us/data/ef.aspx) 访问MSSQL来进行存储


## 安装

### 准备

首先你需要安装 `Visual Studio`, `SQL Server` 和 `Node.js` 以及一个基于Node的个构建工具 `[Grunt.js](gruntjs.com)`.
* [安装 Visual Studio](http://www.visualstudio.com/)
* [安装 SQL Server](http://www.microsoft.com/en-us/sqlserver/default.aspx/)
* [安装 node.js](http://nodejs.org/download/)
* 安装基于全局的node模块: Grunt-CLI  ```npm install -g grunt-cli```

### 获取代码

你可以fock本项目, 也可以直接使用如下命令clone当前代码:

```
git clone https://github.com/TossShinHwa/CMS.git
cd CMS
```

## 运行

因为该项目是前后端分离的, 所以你会看到源码中包括了2个目录: `client`和`server`, 他们使用REST API进行通信.

### 运行后端服务

后端应用程序基于ASP.NET, 所以你需要使用 `nuget` 恢复一些依赖包.

* 使用 Visual Studio 2013 打开解决方案, 其位于 `\server\BlogEngine.By.YangKai.sln`
* 设置启动项目为 `YangKai.BlogEngine.Web.Mvc`
* 使用 `nuget` 恢复缺失的依赖包
* 将配置文件中的 `ConnectionString` 改为您所使用的数据库链接字符串, 其位于 `\server\YangKai.BlogEngine.Web.Mvcweb.config` (至少需要 `MSSQL2005`)
* 编译并运行, 它将自动创建数据库并插入测试数据

### 运行前端应用

前端应用程序是一个纯HTML/Javascript构建的应用程序, 但是在我们需要使用上面提到基于NodeJS的构建工具 [Grunt.js](gruntjs.com) 来运行它. Grunt 又依赖一些第三方 node modules. 使用以下命令, 我们可以安装它们, 并运行应用.

    ```
    cd client
    npm install
    grunt
    ```
    
它将自动打开一个浏览器窗口并链接到后端服务.

## 许可

基于 MIT  协议.
