import React, { Component, StyleSheet, Text, View, ListView, TouchableOpacity } from 'react-native';
import { connect } from 'react-redux/native';
import * as actions from '../redux/posts';

const App  = class App extends Component {
  constructor(props) {
    super(props);
    this.dataSource = new ListView.DataSource({
      rowHasChanged: (row1, row2) => row1 !== row2,
    });
  }

  componentDidMount() {
    this.props.fetchPosts();
  }

  toHome() {
    this.props.router.pop();
  }

  renderPost(post) {
    return (
      <View style={styles.container}>
        <View style={styles.rightContainer}>
          <Text style={styles.title}>{post.title}</Text>
        </View>
      </View>
    );
  }

  render() {
    const { posts } = this.props;
    return (
      <View style={styles.container}>
        <ListView
          dataSource={this.dataSource.cloneWithRows(posts)}
          renderRow={this.renderPost}
          style={styles.listView}
        />
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
  listView: {
    paddingTop: 30,
    backgroundColor: '#F5FCFF',
  },
  title: {
    fontSize: 10,
    marginBottom: 8,
    textAlign: 'left',
  },
});

export default connect(state => state.posts, actions)(App);
