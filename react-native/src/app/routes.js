import App from './framework/App';
import Home from './home/containers/Home';
import Post from './home/containers/Post';

export default class Routes {
  static initialRoute = {
    name: '首页',
    component: Home,
  };

  static routes = {
    home: {
      name: '首页',
      component: Home,
    },
    post: {
      name: '最新文章',
      component: Post,
    }
  };

  constructor(navigator) {
    this.navigator = navigator;
  }

  push(name) {
    const route = Routes.routes[name];
    const currentRoute = this.getCurrentRoute();
    if (route.name !== currentRoute.name) {
      this.navigator.push(route);
    }
  }

  pop() {
    this.navigator.pop();
  }

  getCurrentRoute() {
    const routeList = this.navigator.getCurrentRoutes();
    return routeList[routeList.length - 1];
  }

  isCurrentRoute(routeId) {
    return routeId === getCurrentRoute().id;
  }
}
