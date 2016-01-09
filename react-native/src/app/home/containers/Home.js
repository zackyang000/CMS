import React, { Component, StyleSheet, Text, View, TouchableOpacity } from 'react-native';

export default class App extends Component {
  componentDidMount() {
    this.props.router.push('post');
  }

  toPost() {
    this.props.router.push('post');
  }

  render() {
    return (
      <View style={styles.container}>
        <TouchableOpacity onPress={this.toPost.bind(this)}>
            <Text>Go!!</Text>
        </TouchableOpacity>
      </View>
    );
  }
}

var styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F5FCFF',
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  instructions: {
    textAlign: 'center',
    color: '#333333',
    marginBottom: 5,
  },
});


