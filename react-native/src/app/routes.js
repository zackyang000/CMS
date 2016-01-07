import App from './framework/App';
import Home from './home/containers/Home';

export default function() {
  return {
    defaultRoute: function() {
      return {
        name: 'Home',
        component: Home,
      }
    }
  };
}
