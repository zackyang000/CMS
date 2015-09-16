import { compose, createStore, applyMiddleware } from 'redux';
import { devTools, persistState } from 'redux-devtools';
import thunkMiddleware from 'redux-thunk';
import loggerMiddleware from 'redux-logger';
import diffMiddleware from 'redux-diff-logger';
import requestMiddleware from './../middleware/requestMiddleware';


export default function configureStore(reducers, { apiRoot, isProduction }) {
  let finalCreateStore = undefined;

  const request = requestMiddleware(apiRoot);

  if (isProduction) {
    finalCreateStore = compose(
      applyMiddleware(thunkMiddleware, request)
    )(createStore);
  } else {
    finalCreateStore = compose(
      applyMiddleware(thunkMiddleware, request, diffMiddleware, loggerMiddleware),
      devTools(),
      persistState(window.location.href.match(/[?&]debug=([^&]+)\b/))
    )(createStore);
  }

  return finalCreateStore(reducers);
}
