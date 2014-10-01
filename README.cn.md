***
[ENGLISH README](https://github.com/TossShinHwa/CMS/blob/master/README.md)
***

Blog Engine
===========
[![Build Status](https://api.travis-ci.org/TossShinHwa/CMS.png)](https://api.travis-ci.org/TossShinHwa/CMS)
[![Dependency Status](https://david-dm.org/ChrisWren/grunt-nodemon.png)](https://david-dm.org/TossShinHwa/CMS)
[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/TossShinHwa/cms/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

查看在线演示? 请访问  http://www.woshinidezhu.com/
***

## 目的

为了展示如何使用 [`node-odata`](https://github.com/TossShinHwa/node-odata) 轻松搭建一个基于纯REST通信的**前后端分离**项目。

## 使用的技术

* 酷炫的 [AngularJS](http://www.angularjs.org/)
* CSS使用 [Twitter Bootstrap](http://getbootstrap.com/)
* oData协议的REST基于 [node-odata](https://github.com/TossShinHwa/node-odata) 构建


## 安装

### 准备

您只需要安装 `Node.js` 以及一个基于Node的构建工具 [Grunt](http://gruntjs.com) 和前端组件管理工具 [Bower](http://bower.io/).
* 下载并安装 [node.js](http://nodejs.org/download/)
* 安装基于全局的node模块: Grunt和Bower  ```npm install -g grunt-cli bower```

### 获取代码

使用如下命令clone当前项目:

```
git clone https://github.com/TossShinHwa/CMS.git
cd CMS
```

## 运行

后端应用程序基于NodeJS, 所以你需要使用 `npm` 和 `bower` 恢复一些依赖项.

```
npm install
bower install
```

我们需要使用上面提到基于NodeJS的构建工具 [Grunt.js](gruntjs.com) 来运行它.

```
grunt
```
    
它将自动打开一个浏览器窗口并链接到后端服务.

默认管理员账号: `admin` , 密码: `123`


## 许可

基于 MIT 协议.
