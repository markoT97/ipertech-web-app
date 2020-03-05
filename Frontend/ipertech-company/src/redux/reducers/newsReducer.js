import { FETCH_NEWS } from "../actions/newsActions/actionTypes";

function newsReducer(news = [], action) {
  switch (action.type) {
    case FETCH_NEWS:
      return action.news;
    default:
      return news;
  }
}

export default newsReducer;
