import odata from 'node-odata';
import cors from 'cors';
import path from "path";
import errorHandler from 'errorhandler';
import morgan from 'morgan';

server = odata('mongodb://localhost/cms');

// hack: persistence current all resouces for actions and functions to use.
odata.resources = server.resources;

// odata config
server.use(cors({exposedHeaders: "authorization"}));
server.use(odata._express.bodyParser({uploadDir : path.join(path.dirname(__dirname), 'server/static/upload/temp')}));
server.use(odata._express["static"](path.join(__dirname, "./static")));
server.use(require("./middleware/authorization"));
server.use(morgan("short"));
server.use(errorHandler());

// init resources
server.use(require('./resources/article/article'));
server.use(require('./resources/article/category'));
server.use(require('./resources/article/comment'));
server.use(require('./resources/gallery/gallery'));
server.use(require('./resources/board/board'));
server.use(require('./resources/system/user'));

// init functions
server.use(require('./functions/login'));
server.use(require('./functions/upload'));

// start web server
server.listen(process.env.PORT or 40002, ->
  // import data
  require('./bootstrap/init-data').import()
)
