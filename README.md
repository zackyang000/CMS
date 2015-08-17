Blog Engine 
===========
[![Build Status](https://api.travis-ci.org/TossShinHwa/CMS.png)](https://api.travis-ci.org/TossShinHwa/CMS)
[![Dependency Status](https://david-dm.org/ChrisWren/grunt-nodemon.png)](https://david-dm.org/TossShinHwa/CMS)
[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/TossShinHwa/cms/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

Do you want to see live demo? Visit http://zackyang.com
***

## Purpose

To showcase [`node-odata`](https://github.com/TossShinHwa/node-odata) how easy to write a real project based on communicating with a REST back-end.

## Technology stack

* oData Protocol for REST API base on [node-odata](https://github.com/TossShinHwa/node-odata)
* Awesome [AngularJS](http://www.angularjs.org/)
* CSS based on [Bootstrap](http://getbootstrap.com/)

## Installation

### Prepare

You just need to install Node.js, build tool named [Grunt](http://gruntjs.com) and package manage tool named [Bower](http://bower.io/).
* [Install node.js](http://nodejs.org/download/)
* Install `Grunt` and `Bower` as global npm modules ```npm install -g grunt-cli bower```

### Get the Code

Either clone this repository or fork it on GitHub and clone your fork:

```
git clone https://github.com/TossShinHwa/CMS.git
cd CMS
```

## Run

We need to install as local dependencies using npm and bower.

```
npm install
bower install
```

Then run it

```
grunt
```

It will automatically open a browser and connect to the server.

Default admin account: `admin` , password: `123`


## License

MIT License.
