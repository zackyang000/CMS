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

var styles = StyleSheet.create({
  item: {
    flex: 1,
    justifyContent: 'center',
    paddingBottom: 15,
    paddingTop: 10,
    paddingLeft: 10,
    paddingRight: 10,
  },
  title: {
    fontSize: 18,
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

