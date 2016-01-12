import Home from './home/containers/Home';
import PostList from './post/containers/List';
import PostDetail from './post/containers/Detail';

export default class Routes {
  static initialRoute = {
    name: '首页',
    component: Home,
  };

  // <Route path="world" component={World} />

  static routes = {
    home: Home,
    post: PostList,
    'post/detail': PostDetail,
  };

  constructor(navigator) {
    this.navigator = navigator;
  }

  push(name, params) {
    this.navigator.push({
      component: Routes.routes[name],
      ...params
    });
  }

  pop() {
    this.navigator.pop();
  }
}
