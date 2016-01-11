import React, { Component, StyleSheet, ScrollView, Text, View, ListView, TouchableOpacity } from 'react-native';
import Dimensions from 'Dimensions';
import { connect } from 'react-redux/native';
import HTMLView from 'react-native-htmlview';
import * as actions from '../redux/post';
import Header from '../../framework/Header';

const App = class App extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.fetchPostByUrl(this.props.params.url);
  }

  toList() {
    this.props.router.pop();
  }

  render() {
    let { post = {}, params } = this.props;
    post = post[this.props.params.url] || {};
    const { width, height } = Dimensions.get('window');
    return (
      <View style={{width, height}}>
        <Header />
        <ScrollView>
          <View style={styles.titleView}>
            <Text style={styles.title}>{post.title}</Text>
          </View>
          <View style={styles.contentView}>
            {post.content &&
              <HTMLView value={post.content} stylesheet={styles} />
            }
          </View>
        </ScrollView>
      </View>
    );
  }
}

var styles = StyleSheet.create({
  title: {
    padding: 10,
    fontSize: 18,
    fontWeight: "700",
  },
  contentView: {
    padding: 10,
    flex: 1,
  },
  content: {
  },
  p: {
    fontSize: 16,
    lineHeight: 23,
    padding: 0,
    margin: 0,
  },
});

export default connect(state => state.post, actions)(App);

