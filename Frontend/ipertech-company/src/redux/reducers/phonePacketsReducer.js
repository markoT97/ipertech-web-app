import { FETCH_PHONE_PACKETS } from "../actions/phonePacketsActions/actionTypes";

function phonePacketsReducer(phonePackets = [], action) {
  switch (action.type) {
    case FETCH_PHONE_PACKETS:
      return action.phonePackets;
    default:
      return phonePackets;
  }
}

export default phonePacketsReducer;
