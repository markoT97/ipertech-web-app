import {
  FETCH_PACKET_COMBINATIONS,
  INSERT_PACKET_COMBINATIONS,
  DELETE_PACKET_COMBINATION,
} from "../actions/packetCombinationsActions/actionTypes";

function packetCombinationsReducer(packetCombinations = [], action) {
  switch (action.type) {
    case FETCH_PACKET_COMBINATIONS:
      return action.packetCombinations;
    case INSERT_PACKET_COMBINATIONS:
      return [...packetCombinations, action.newPacketCombination];
    case DELETE_PACKET_COMBINATION:
      return [
        ...packetCombinations.filter(
          (packet) => packet.packetCombinationId !== action.packetCombinationId
        ),
      ];
    default:
      return packetCombinations;
  }
}

export default packetCombinationsReducer;
