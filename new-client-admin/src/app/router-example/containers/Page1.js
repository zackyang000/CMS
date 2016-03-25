import React, { Component } from 'react';
import { Button, router } from 'cuz';
import Helmet from 'react-helmet';

export default class App extends Component {
  render() {
    return (
      <div>
        <Helmet title="Page1" />
        <h3>This is router-example-Page1</h3>
        <div><Button onClick={::this._goToPage2}>Go to Page 2</Button></div>
        <div><Button onClick={::this._goToPage2WithRouteParams}>Go with route params</Button></div>
        <div><Button onClick={::this._goToPage2WithQueryString}>Go with query string</Button></div>
        <div><Button onClick={::this._goToPage2WithState}>Go with state</Button></div>
      </div>
    );
  }

  _goToPage2() {
    router.push('/router-example/page2');
  }

  _goToPage2WithRouteParams() {
    router.push('/router-example/page2/123');
  }

  _goToPage2WithQueryString() {
    router.push({
      pathname: '/router-example/page2',
      query: { modal: true },
    });
  }

  _goToPage2WithState() {
    router.push({
      pathname: '/router-example/page2',
      state: { fromDashboard: true }
    });
  }
}

