import React, { Component, StyleSheet, Text, View, TouchableOpacity } from 'react-native';
import styles from './Header.styles';

export default class App extends Component {
  componentDidMount() {
  }

  toPost() {

  }

  render() {
    return (
      <View style={styles.container}>
        <View>
          {this.props.leftContainer}
        </View>
        <View>
          {this.props.centerContainer}
        </View>
        <View>
          {this.props.rightContainer}
        </View>
      </View>
    );
  }
}

