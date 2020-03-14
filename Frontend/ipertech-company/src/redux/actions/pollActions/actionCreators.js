import { FETCH_THE_LATEST_POLL } from "./actionTypes";
import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

export function fetchLatestPoll() {
  return dispatch => {
    axios.get(BACKEND_URL + "api/polls").then(response => {
      dispatch({ type: FETCH_THE_LATEST_POLL, polls: response.data });
    });
  };
}
