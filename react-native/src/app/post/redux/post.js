import { createReducer } from 'cuz';

const FETCH_POST = 'fetch post';

export function fetchPostByUrl(url) {
  return {
    type: FETCH_POST,
    promise: (request) => request.get(`/articles?$filter=url eq '${url}'`),
    url,
  };
}

const initState = {
}

export default createReducer(initState, {
  [FETCH_POST](state, action) {
    return {
      ...state,
      post: {
        ...state.post,
        [action.url]: action.result.value[0],
      }
    }
  }
});


