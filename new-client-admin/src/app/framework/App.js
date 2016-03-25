import React, { Component, PropTypes } from 'react';
import Header from './Header';
import Footer from './Footer';
import Sider from './Sider';
import Nav from './Nav';

export default class App extends Component {
  static propTypes = {
    children: PropTypes.object,
  };

  render() {
    return (
      <div>
        <Header />
        <div className="wrapper">
          <Sider>
            <Nav />
          </Sider>
          <div className="main">
            {this.props.children}
          </div>
          <Footer />
        </div>
      </div>
    );
  }
}
