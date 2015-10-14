import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router';
import * as actions from '../../redux/articles';
import ArticlesList from '../components/ArticlesList';

@connect(state => state.articles, actions)
export default class App extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.fetchArticles();
  }

  render() {
    const { articles } = this.props;
    return (
      <div>
        <div className="content">
          <ArticlesList articles={articles} />
        </div>
      </div>
    );
  }
}

