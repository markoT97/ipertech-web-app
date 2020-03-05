import { combineReducers } from "redux";
import promotionsReducer from "./../reducers/promotionsReducer";
import newsReducer from "./../reducers/newsReducer";

const rootReducer = combineReducers({
  promotions: promotionsReducer,
  news: newsReducer
});

export default rootReducer;
