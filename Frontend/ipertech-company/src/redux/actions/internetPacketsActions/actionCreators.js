import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_INTERNET_PACKETS } from "./actionTypes";

function fetchInternetPackets() {
  return dispatch => {
    fetch(BACKEND_URL + "api/internetPackets")
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(internetPackets => {
        dispatch({
          type: FETCH_INTERNET_PACKETS,
          internetPackets
        });
      })
      .catch(err => {
        console.error(err);
      });
  };
}
export default fetchInternetPackets;
