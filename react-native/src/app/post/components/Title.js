import styles from './Title.styles';
import React, { Component, StyleSheet, Text, View, ListView, TouchableOpacity } from 'react-native';
import moment from 'moment';
import 'moment/locale/zh-cn';

export default class App extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { post } = this.props;
    return (
      <View style={styles.item}>
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
    );
  }
}
