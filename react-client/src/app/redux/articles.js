const FETCH_ARTICLES = 'fetch articles';

export function fetchArticles() {
  return (dispatch, getState) => {
    return dispatch({
      type: FETCH_ARTICLES,
      promise: (request) => request.get(`/articles?$top=10&$select=title,date`)
    });
  };
}

const initState = {
  articles: [],
}

export default function reducer(state = initState, action) {
  if (action.readyState === 'success') {
    switch (action.type) {
      case FETCH_ARTICLES:
        switch (action.readyState) {
          case 'success':
            return {
              ...state,
              articles: action.result.value,
          }
      }
      default:
        return state;
    }
  }
  return state;
}

