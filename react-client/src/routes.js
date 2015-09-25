import React, { Component } from 'react';
import { Route, IndexRoute } from 'react-router';
import menus from './menus';
import App from './onekit/containers/App';
import Home from './app/home/containers/Home';

export default function() {
  return (
    <Route component={App}>
      <Route path="/" component={Home} />
    </Route>
  );
}
