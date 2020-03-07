import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_PHONE_PACKETS } from "./actionTypes";

function fetchPhonePackets() {
  return dispatch => {
    fetch(BACKEND_URL + "api/phonePackets")
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(phonePackets => {
        dispatch({
          type: FETCH_PHONE_PACKETS,
          phonePackets
        });
      })
      .catch(err => {
        console.error(err);
      });
  };
}
export default fetchPhonePackets;
