import React, { Component } from 'react-native';

export default class App extends Component {
  render() {
    return (
      <View>{this.props.children}</View>
    );
  }
}
