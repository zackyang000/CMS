import './index.scss';
import 'babel-core/polyfill';
import React from 'react';
import { Provider } from 'react-redux';
import { Router } from 'react-router';
import { DevTools, DebugPanel, LogMonitor } from 'redux-devtools/lib/react';
import createRoutes from './routes';
import reducers from './app/reducers';
import config from './config';

const store = createStore(reducers, config);

if (!config.isProduction && module.hot) {
  module.hot.accept('./app/reducers', () => {
    const nextRootReducer = require('./app/reducers');
    store.replaceReducer(nextRootReducer);
  });
}

const routes = createRoutes();

React.render(
  <div>
    <Provider store={store}>
      { () => <Router children={routes} /> }
    </Provider>
    { config.enableDevTools &&
      <DebugPanel top right bottom>
        <DevTools store={store} monitor={LogMonitor} />
      </DebugPanel>
    }
  </div>
, document.getElementById('root'));
