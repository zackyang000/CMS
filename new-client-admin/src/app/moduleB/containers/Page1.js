import React, { Component } from 'react';
import Helmet from 'react-helmet';

export default class App extends Component {
  render() {
    return (
      <div>
        <Helmet title="Page1" />
        <h3>This is moduleB-Page1!</h3>
      </div>
    );
  }
}

