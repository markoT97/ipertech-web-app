import { combineReducers } from "redux";
import promotionsReducer from "./../reducers/promotionsReducer";
import newsReducer from "./../reducers/newsReducer";
import internetPacketsReducer from "./../reducers/internetPacketsReducer";
import tvPacketsReducer from "./../reducers/tvPacketsReducer";

const rootReducer = combineReducers({
  promotions: promotionsReducer,
  news: newsReducer,
  internetPackets: internetPacketsReducer,
  tvPackets: tvPacketsReducer
});

export default rootReducer;
