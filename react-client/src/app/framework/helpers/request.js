import superagent from 'superagent';

export default class Request {
  constructor(apiRoot) {
    ['get', 'post', 'put', 'patch', 'del'].
      map((method) => {
        this[method] = (path, options) => {
          return new Promise((resolve, reject) => {
            const request = superagent[method](this.formatUrl(path, apiRoot));
            if (options && options.query) {
              request.query(options.query);
            }
            if (options && options.data) {
              request.send(options.data);
            }
            request.end((err, res) => {
              if (err) {
                reject((res && res.body) || err);
              } else {
                resolve(res.body);
              }
            });
          });
        };
      });
  }

  formatUrl(path, apiRoot) {
    path = path[0] !== '/' ? '/' + path : path;
    return apiRoot + path;
  }
}

