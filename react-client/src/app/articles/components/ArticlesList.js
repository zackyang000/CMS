import React, { Component } from 'react';
import { Link } from 'react-router';

export default class Comp extends Component {
  render() {
    return (
        <ul>
          {this.props.libraries.map((item, j) =>
            <li key={j}>
              {item.title}
            </li>
          )}
        </ul>
    );
  }
}

