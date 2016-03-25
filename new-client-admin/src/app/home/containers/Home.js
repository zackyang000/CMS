import React, { Component } from 'react';
import Helmet from 'react-helmet';

export default class App extends Component {
  render() {
    return (
      <div>
        <Helmet title="Home Page" />
        <h3>Welcome home!</h3>
      </div>
    );
  }
}
