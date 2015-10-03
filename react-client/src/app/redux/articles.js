const FETCH_ARTICLES = 'fetch articles';

export function fetchArticles() {
  return (dispatch, getState) => {
    return dispatch({
      type: FETCH_ARTICLES,
      promise: (request) => request.get(`/articles`)
    });
  };
}

const initState = {
  articles: [],
}

export default function reducer(state = initState, action) {
  switch (action.type) {
    case FETCH_ARTICLES:
      switch (action.readyState) {
        case 'request':
          return {
            ...state,
        }
        case 'success':
          return {
            ...state,
            articles: action.data,
        }
    }
    default:
      return state;
  }
}

