import { combineReducers } from "redux";
import promotionsReducer from "./../reducers/promotionsReducer";
import newsReducer from "./../reducers/newsReducer";
import internetPacketsReducer from "./../reducers/internetPacketsReducer";
import tvPacketsReducer from "./../reducers/tvPacketsReducer";
import phonePacketsReducer from "./../reducers/phonePacketsReducer";
import packetCombinationsReducer from "./../reducers/packetCombinationsReducer";
import loginModalVisibilityReducer from "./../reducers/loginModalReducer";
import authReducer from "./authReducer";
import userReducer from "./userReducer";
import pollReducer from "./pollsReducer";

const rootReducer = combineReducers({
  promotions: promotionsReducer,
  news: newsReducer,
  internetPackets: internetPacketsReducer,
  tvPackets: tvPacketsReducer,
  phonePackets: phonePacketsReducer,
  packetCombinations: packetCombinationsReducer,
  loginModalVisibility: loginModalVisibilityReducer,
  auth: authReducer,
  user: userReducer,
  poll: pollReducer
});

export default rootReducer;
