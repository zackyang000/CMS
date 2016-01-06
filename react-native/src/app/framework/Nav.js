import './Nav.css';
import React, { Component } from 'react';
import { Link } from 'react-router';
import menus from '../menus';

export default class App extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
  }

  render() {
    return (
      <nav className="menu">
        <ul>
          {menus.map((menu, i) =>
            <li key={i}>
              {menu.url &&
                <Link to={menu.url} activeClassName="current">
                  <i className={`fa fa-fw fa-${menu.icon}`}></i>
                  {menu.name}
                </Link>
              }
              {menu.children && menu.children.length >0 &&
                <div>
                  <small>
                    <i className={`fa fa-fw fa-${menu.icon}`}></i>
                    {menu.name}
                  </small>
                  <ul>
                    {menu.children.map((subMenu, j) =>
                      <li key={j}><Link to={subMenu.url} activeClassName="current">{subMenu.name}</Link></li>
                    )}
                  </ul>
                </div>
              }
            </li>
          )}
        </ul>
      </nav>
    );
  }
}
