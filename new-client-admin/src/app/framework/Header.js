import './Header.css';
import React, { Component } from 'react';

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
          LOGO
        </div>
      </header>
    );
  }
}
