import odata from 'node-odata';
import cors from 'cors';
import path from 'path';
import errorHandler from 'errorhandler';
import morgan from 'morgan';
import bodyParser from 'body-parser';

import authorizationMiddleware from './middleware/authorization';

import article from './resources/article/article';
import category from './resources/article/category';
import comment from './resources/article/comment';
import gallery from './resources/gallery/gallery';
import board from './resources/board/board';
import user from './resources/system/user';

import login from './functions/login';
import upload from './functions/upload';

const server = odata('mongodb://localhost/cms');

// hack: persistence current all resouces for actions and functions to use.
odata.resources = server.resources;

// odata config
server.use(cors({ exposedHeaders: 'authorization' }));
server.use(bodyParser({
  uploadDir: path.join(path.dirname(__dirname), 'server/static/upload/temp'),
}));
server.use(odata._express.static(path.join(__dirname, './static')));
server.use(authorizationMiddleware);
server.use(morgan('short'));
server.use(errorHandler());

// init resources
[
  article,
  category,
  comment,
  gallery,
  board,
  user,
].map((resource) => server.use(resource));

// init functions
[
  login,
  upload,
].map((resource) => server.use(resource));

// start web server
server.listen(process.env.PORT || 40002, () =>
  // import data
  require('./bootstrap/init-data').import()
);
