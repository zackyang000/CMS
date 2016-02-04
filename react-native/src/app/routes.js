import React from 'react-native';
import { Route } from 'cuz-native';
import Home from './home/containers/Home';
import PostList from './post/containers/List';
import PostDetail from './post/containers/Detail';

export default function() {
  return (
    <Route>
      <Route default path="home" component={Home} />
      <Route path="post" component={PostList} />
      <Route path="post/detail" component={PostDetail} />
    </Route>
  );
}
