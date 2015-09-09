import React, { Component } from 'react';
import { Route, IndexRoute } from 'react-router';
import warp from './onekit/helpers/wrapComponent';
import menus from './menus';
import App from './onekit/containers/App';
import Home from './app/home/containers/Home';

export default function() {
  return (
    <Route component={warp(App, { menus })}>
      <Route path="/" component={Home} />
    </Route>
  );
}
