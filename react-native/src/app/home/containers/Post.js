import React, { Component, StyleSheet, Text, View, TouchableOpacity } from 'react-native';
import { connect } from 'react-redux/native';
import * as actions from '../redux/posts';

const App  = class App extends Component {
  componentDidMount() {
    console.log(this.props);
    this.props.fetchPosts();
  }

  toHome() {
    this.props.router.pop();
  }

  render() {
    const { posts } = this.props;
    console.log(posts);
    return (
      <View style={styles.container}>
        <TouchableOpacity onPress={this.toHome.bind(this)}>
            <Text>Back</Text>
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

export default connect(state => state.posts, actions)(App);
