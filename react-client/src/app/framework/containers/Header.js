import './Header.scss';
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
      <header>
        <div className="logo" style={{color: '#fff'}}>
          DAAS LOGO
        </div>
      </header>
    );
  }
}
