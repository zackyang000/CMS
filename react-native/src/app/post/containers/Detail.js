import styles from './Detail.styles';
import React, { Component, StyleSheet, ScrollView, Text, View, ListView, TouchableOpacity } from 'react-native';
import Dimensions from 'Dimensions';
import { connect } from 'react-redux/native';
import HTMLView from 'react-native-htmlview';
import * as actions from '../redux/post';
import Header from '../../framework/Header';
import Title from '../components/Title';

const App = class App extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.fetchPostByUrl(this.props.params.post.url);
  }

  toList() {
    this.props.router.pop();
  }

  render() {
    let { post = {}, params } = this.props;
    post = post[this.props.params.post.url] || this.props.params.post;
    const { width, height } = Dimensions.get('window');
    return (
      <View style={{width, height}}>
        <Header />
        <ScrollView>
          <Title post={post} />
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

export default connect(state => state.post, actions)(App);

