import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_TV_PACKETS } from "./actionTypes";

function fetchTvPackets() {
  return dispatch => {
    fetch(BACKEND_URL + "api/tvPackets")
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(tvPackets => {
        dispatch({
          type: FETCH_TV_PACKETS,
          tvPackets
        });
      })
      .catch(err => {
        console.error(err);
      });
  };
}
export default fetchTvPackets;
