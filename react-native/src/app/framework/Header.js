import React, { Component, StyleSheet, Text, View, TouchableOpacity } from 'react-native';

export default class App extends Component {
  componentDidMount() {
  }

  toPost() {

  }

  render() {
    return (
      <View style={styles.container}>
        <View>
          <TouchableOpacity onPress={this.toPost.bind(this)}>
            <Text>&lt;&lt;</Text>
          </TouchableOpacity>
        </View>
        <View>
            <Text>Title</Text>
        </View>
        <View>
          <TouchableOpacity onPress={this.toPost.bind(this)}>
            <Text>Right</Text>
          </TouchableOpacity>
        </View>
      </View>
    );
  }
}

var styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    flexDirection: 'row',
    justifyContent: 'space-between',
    borderBottomColor: '#ccc',
    borderBottomWidth: 1,
    paddingTop: 15,
  },
});



