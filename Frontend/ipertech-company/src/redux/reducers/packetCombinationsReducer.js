import { FETCH_PACKET_COMBINATIONS } from "../actions/packetCombinationsActions/actionTypes";

function packetCombinationsReducer(packetCombinations = [], action) {
  switch (action.type) {
    case FETCH_PACKET_COMBINATIONS:
      return action.packetCombinations;
    default:
      return packetCombinations;
  }
}

export default packetCombinationsReducer;
