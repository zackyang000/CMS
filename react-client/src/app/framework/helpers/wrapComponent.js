import React, { Component } from 'react';

export default function(comp, props) {
  return class App extends Component {
    render() {
      return React.createElement(comp, props, this.props.children);
    }
  }
};
