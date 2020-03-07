import { FETCH_INTERNET_PACKETS } from "../actions/internetPacketsActions/actionTypes";

function internetPacketsReducer(internetPackets = [], action) {
  switch (action.type) {
    case FETCH_INTERNET_PACKETS:
      return action.internetPackets;
    default:
      return internetPackets;
  }
}

export default internetPacketsReducer;
