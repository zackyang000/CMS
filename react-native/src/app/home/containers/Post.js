import React, { Component, StyleSheet, Text, View, ListView, TouchableOpacity } from 'react-native';
import Dimensions from 'Dimensions';
import moment from 'moment';
import 'moment/locale/zh-cn';
import { connect } from 'react-redux/native';
import * as actions from '../redux/posts';
import Header from '../../framework/Header';

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
      <View>
        <View style={styles.item}>
          <View>
            <Text style={styles.title}>
              {post.title}
            </Text>
            <View style={styles.sub}>
              <Text style={styles.author}>
                {post.meta.author}
              </Text>
              <Text style={styles.date}>
                {moment(post.date).fromNow()}
              </Text>
            </View>
          </View>
        </View>
        <View style={styles.separator} />
      </View>
    );
  }

  render() {
    const { posts } = this.props;
    const { width, height } = Dimensions.get('window');
    return (
      <View style={{width}}>
        <View style={styles.header}>
          <Header />
        </View>
        <ListView
          dataSource={this.dataSource.cloneWithRows(posts)}
          renderRow={this.renderPost}
          style={styles.listView}
          initialListSize={4}
        />
        <View style={styles.footer}>
          <TouchableOpacity onPress={this.toHome.bind(this)}>
              <Text>Back</Text>
          </TouchableOpacity>
        </View>
      </View>
    );
  }
}

var styles = StyleSheet.create({
  header: {
    flex: 2,
  },
  listView: {
    flex: 18,
    backgroundColor: '#F5FCFF',
  },
  footer: {
    flex: 1,
  },
  item: {
    flex: 1,
    justifyContent: 'center',
    paddingBottom: 15,
    paddingTop: 20,
    paddingLeft: 10,
    paddingRight: 10,
    backgroundColor: '#F6F6F6',
  },
  title: {
    fontSize: 16,
    lineHeight: 23,
    marginBottom: 2,
    textAlign: 'left',
  },
  sub: {
    marginTop: 8,
    flexDirection: 'row',
  },
  author: {
    fontSize: 12,
    textAlign: 'left',
    color: "#999",
  },
  date: {
    fontSize: 12,
    textAlign: 'left',
    marginLeft: 10,
    color: "#999",
  },
  separator: {
    flex: 1,
    height: 1,
    backgroundColor: '#ccc',
  },
});

export default connect(state => state.posts, actions)(App);
