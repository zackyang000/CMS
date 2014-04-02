BlogEngine base AngularJS
===========
[![Build Status](https://api.travis-ci.org/TossShinHwa/CMS.png)](https://api.travis-ci.org/TossShinHwa/CMS)
[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/TossShinHwa/cms/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

Do you want to see live demo? Visit http://woshinidezhu.com
***

## Purpose

To showcase AngularJS how to write a real project based on communicating with a REST back-end.

## Technology stack

* Awesome [AngularJS](http://www.angularjs.org/)
* CSS based on [Bootstrap](http://getbootstrap.com/)
* REST API based on [ASP.NET WEB API](http://www.asp.net/web-api/)
* Persistence store used [Entity Framework](http://msdn.microsoft.com/en-us/data/ef.aspx) to visit MSSQL


## Installation

### Prepare

You need to install Visual Studio and Node.js.
* [Install Visual Studio](http://www.visualstudio.com/)
* [Install node.js](http://nodejs.org/download/)
* Install Grunt-CLI as global npm modules ```npm install -g grunt-cli```

### Get the Code

Either clone this repository or fork it on GitHub and clone your fork:

```
git clone https://github.com/TossShinHwa/CMS.git
cd CMS
```

### Run App Server

Backend application server is a ASP.NET application that relies upon some 3rd Party nuget packages.

* Open the server side solution using Visual Studio 2013
* Use nuget restore lost packages
* Modify the `ConnectionString` at `web.config` under Mvc project (Required above `MSSQL2005` or above)
* Compile and run, it will automatically create the database and insert test data

### Run Client App

Client application is a straight HTML/Javascript application but our development process uses a Node.js build tool
[Grunt.js](gruntjs.com). Grunt relies upon some 3rd party libraries that we need to install as local dependencies using npm.

    ```
    cd client
    npm install
    grunt
    ```
    
It will automatically open a browser and connect to the server.

## License

MIT License.
