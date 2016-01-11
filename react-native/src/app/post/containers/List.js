import React, { Component, StyleSheet, Text, View, ListView, TouchableOpacity } from 'react-native';
import Dimensions from 'Dimensions';
import { connect } from 'react-redux/native';
import * as actions from '../redux/posts';
import Header from '../../framework/Header';
import Item from '../components/Item';

const App = class App extends Component {
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

  render() {
    const { posts } = this.props;
    const { width, height } = Dimensions.get('window');
    return (
      <View style={{width, height}}>
        <View style={styles.header}>
          <Header
            leftContainer={
              <TouchableOpacity onPress={this.toHome.bind(this)}>
                <Text>返回</Text>
              </TouchableOpacity>
            }
            centerContainer={
              <Text>最新文章</Text>
            }
          />
        </View>
        <ListView
          dataSource={this.dataSource.cloneWithRows(posts)}
          renderRow={(post) => <Item post={post} />}
          style={styles.listView}
          initialListSize={4}
        />
      </View>
    );
  }
}

var styles = StyleSheet.create({
  listView: {
    flex: 18,
    backgroundColor: '#F5FCFF',
  },
});

export default connect(state => state.posts, actions)(App);
