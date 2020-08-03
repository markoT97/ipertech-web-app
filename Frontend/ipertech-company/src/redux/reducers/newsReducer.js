import {
  FETCH_NEWS,
  INSERT_NEWS,
  DELETE_NEWS,
} from "../actions/newsActions/actionTypes";

function newsReducer(news = [], action) {
  switch (action.type) {
    case FETCH_NEWS:
      return action.news;
    case INSERT_NEWS:
      return [action.newNews, ...news.slice(1, 3)];
    case DELETE_NEWS:
      return [
        ...news.filter((n) => n.notificationId !== action.notificationId),
      ];
    default:
      return news;
  }
}

export default newsReducer;
