import App from './framework/App';
import Home from './home/containers/Home';
import Post from './home/containers/Post';

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

  pop() {
    this.navigator.pop();
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

  toPost() {
    this.push({
      id: 'post',
      title: '文章列表',
      component: Post
    });
  }
}
