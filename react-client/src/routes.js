import React, { Component } from 'react';
import { Route, IndexRoute } from 'react-router';
import App from './app/framework/containers/App';
import Home from './app/home/containers/Home';

export default function() {
  return (
    <Route component={App}>
      <Route path="/" component={Home} />
    </Route>
  );
}
