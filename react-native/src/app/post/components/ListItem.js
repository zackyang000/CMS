import React, { Component, StyleSheet, Text, View, ListView, TouchableOpacity } from 'react-native';
import Title from './Title';

export default class App extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { post } = this.props;
    return (
      <View>
        <TouchableOpacity activeOpacity={1} onPress={this.props.toDetail.bind(this, post)}>
          <Title post={post} />
        </TouchableOpacity>
        <View style={styles.separator} />
      </View>
    );
  }
}

var styles = StyleSheet.create({
  separator: {
    flex: 1,
    height: 1,
    backgroundColor: '#f5f5f5',
  },
});
