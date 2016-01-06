import './Sider.css';
import React, { Component } from 'react';
import { Link } from 'react-router';

export default class App extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
  }

  render() {
    return (
      <div className="sider">
        {this.props.children}
      </div>
    );
  }
}
