import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_USER_BY_ID } from "./actionTypes";

export function fetchUserById(id) {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/users/" + id)
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch({ type: FETCH_USER_BY_ID, user: response.data });
      })
      .catch(error => {
        console.error(error);
      });
  };
}
