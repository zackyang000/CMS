import React, { Component } from 'react';
import Helmet from 'react-helmet';

export default class App extends Component {
  render() {
    return (
      <div>
        <Helmet title="Page2" />
        <h3>This is moduleB-Page2!</h3>
      </div>
    );
  }
}

