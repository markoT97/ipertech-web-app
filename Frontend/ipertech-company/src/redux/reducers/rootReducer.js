import { combineReducers } from "redux";
import promotionsReducer from "./../reducers/promotionsReducer";
import newsReducer from "./../reducers/newsReducer";
import internetPacketsReducer from "./../reducers/internetPacketsReducer";

const rootReducer = combineReducers({
  promotions: promotionsReducer,
  news: newsReducer,
  internetPackets: internetPacketsReducer
});

export default rootReducer;
