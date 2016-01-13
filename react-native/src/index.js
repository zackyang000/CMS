import cuz from 'cuz/native';
import config from './config';
import reducers from './app/reducers';
import routes from './app/routes';

export default function() {
  return cuz(reducers, routes, config); 
}
