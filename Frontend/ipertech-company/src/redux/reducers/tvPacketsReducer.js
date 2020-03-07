import { FETCH_TV_PACKETS } from "../actions/tvPacketsActions/actionTypes";

function tvPacketsReducer(tvPackets = [], action) {
  switch (action.type) {
    case FETCH_TV_PACKETS:
      return action.tvPackets;
    default:
      return tvPackets;
  }
}

export default tvPacketsReducer;
