import Request from './../helpers/request';

export default function requestMiddleware(apiRoot) {
  const request = new Request(apiRoot);

  return ({dispatch, getState}) => {
    return next => action => {
      if (typeof action === 'function') {
        return action(dispatch, getState);
      }

      const { promise, type, ...params } = action;
      if (!promise) {
        return next(action);
      }

      next({...params, type, readyState: 'request'});

      return promise(request).then(
        (result) => next({...params, result, type, readyState: 'success'}),
        (error) => next({...params, error, type, readyState: 'failure'})
      ).catch((error)=> {
        console.error('requestMiddleware: ', error);
        next({...params, error, type, readyState: 'failure'});
      });
    };
  }
}
