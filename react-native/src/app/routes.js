import App from './framework/App';
import Home from './home/containers/Home';

export default class Routes {
  static initialRoute = {
    name: 'Home',
    component: Home,
  };

  constructor(navigator) {
    this.navigator = navigator;
  }

  push(route) {
    let currentRoute = this.getCurrentRoute();
    if (route.id !== currentRoute.id) {
      this.navigator.push(route);
    }
  }

  getCurrentRoute() {
    let routeList = this.navigator.getCurrentRoutes();
    return routeList[routeList.length - 1];
  }

  isCurrentRoute(routeId) {
    return routeId === getCurrentRoute().id;
  }

  toHome() {
    this.push({
      id: 'home',
      title: '最新',
      component: Home
    });
  }
}
