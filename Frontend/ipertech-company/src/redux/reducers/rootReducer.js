import { combineReducers } from "redux";
import promotionsReducer from "./../reducers/promotionsReducer";
import newsReducer from "./../reducers/newsReducer";
import internetPacketsReducer from "./../reducers/internetPacketsReducer";
import tvPacketsReducer from "./../reducers/tvPacketsReducer";
import phonePacketsReducer from "./../reducers/phonePacketsReducer";
import packetCombinationsReducer from "./../reducers/packetCombinationsReducer";
import modalsVisibilityReducer from "./../reducers/modalsReducer";
import authReducer from "./authReducer";
import userReducer from "./userReducer";
import pollReducer from "./pollsReducer";
import billsReducer from "./billsReducer";
import tableOfBillsReducer from "./tableOfBillsReducer";
import messagesReducer from "./messagesReducer";
import notificationsReducer from "./notificationsReducer";
import userContractsReducer from "./userContractsReducer";

const rootReducer = combineReducers({
  promotions: promotionsReducer,
  news: newsReducer,
  internetPackets: internetPacketsReducer,
  tvPackets: tvPacketsReducer,
  phonePackets: phonePacketsReducer,
  packetCombinations: packetCombinationsReducer,
  modalsVisibility: modalsVisibilityReducer,
  auth: authReducer,
  user: userReducer,
  poll: pollReducer,
  bills: billsReducer,
  tableOfBills: tableOfBillsReducer,
  userMessages: messagesReducer,
  notifications: notificationsReducer,
  userContracts: userContractsReducer,
});

export default rootReducer;
