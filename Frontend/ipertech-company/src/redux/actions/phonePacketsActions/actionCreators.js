import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_PHONE_PACKETS } from "./actionTypes";

function fetchPhonePackets() {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/phonePackets")
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch({
          type: FETCH_PHONE_PACKETS,
          phonePackets: response.data
        });
      })
      .catch(error => {
        console.error(error);
      });
  };
}
export default fetchPhonePackets;
