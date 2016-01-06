import './index.css';
import cuz from 'cuz';
import config from './config';
import reducers from './app/reducers';
import routes from './app/routes';

cuz(reducers, routes, config);
