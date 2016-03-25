import './Page2.css';
import React, { Component, PropTypes } from 'react';
import { Button, router } from 'cuz';
import Helmet from 'react-helmet';

export default class App extends Component {
  static propTypes = {
    location: PropTypes.object,
    params: PropTypes.object,
  };

  render() {
    const { location, params } = this.props;
    return (
      <div>
        <Helmet title="Page2" />
        <h3>This is router-example-Page2</h3>
        <div><Button onClick={::this._goBack}>Go back</Button></div>
        <div>
          <div>
            Route Params:
            <span className="page-pass-value">
              {JSON.stringify(params)}
            </span>
          </div>
          <div>
            Query String:
            <span className="page-pass-value">
              {JSON.stringify(location.query)}
            </span>
          </div>
          <div>
            State:
            <span className="page-pass-value">
              {JSON.stringify(location.state || {})}
            </span>
          </div>
        </div>
      </div>
    );
  }

  _goBack() {
    router.goBack();
  }
}

