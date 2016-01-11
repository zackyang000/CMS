import Home from './home/containers/Home';
import PostList from './post/containers/List';
import PostDetail from './post/containers/Detail';

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
      name: '文章列表',
      component: PostList,
    },
    'post/detail': {
      name: '文章明细',
      component: PostDetail,
    },
  };

  constructor(navigator) {
    this.navigator = navigator;
  }

  push(name, params) {
    const route = Routes.routes[name];
    const currentRoute = this.getCurrentRoute();
    if (route.name !== currentRoute.name) {
      this.navigator.push({
        ...route,
        ...params
      });
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
