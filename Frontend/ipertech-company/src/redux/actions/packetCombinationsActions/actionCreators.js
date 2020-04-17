import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_PACKET_COMBINATIONS } from "./actionTypes";

function fetchPacketCombinations() {
  return dispatch => {
    fetch(BACKEND_URL + "api/packetCombinations")
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(packetCombinations => {
        dispatch({
          type: FETCH_PACKET_COMBINATIONS,
          packetCombinations
        });
      })
      .catch(err => {
        //console.error(err);
      });
  };
}
export default fetchPacketCombinations;
