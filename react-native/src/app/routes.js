import React, { Component } from 'react';
import { Route, IndexRoute } from 'react-router';
import App from './framework/App';
import Home from './home/containers/Home';
import ModuleAPage1 from './moduleA/containers/Page1';
import ModuleAPage2 from './moduleA/containers/Page2';
import ModuleBPage1 from './moduleB/containers/Page1';
import ModuleBPage2 from './moduleB/containers/Page2';
import ModuleBPage3 from './moduleB/containers/Page3';

export default function() {
  return (
    <Route component={App}>
      <Route path="/" component={Home} />
      <Route path="module-a/page1" component={ModuleAPage1} />
      <Route path="module-a/page2" component={ModuleAPage2} />
      <Route path="module-b/page1" component={ModuleBPage1} />
      <Route path="module-b/page2" component={ModuleBPage2} />
      <Route path="module-b/page3" component={ModuleBPage3} />
    </Route>
  );
}
