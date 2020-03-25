import {
  FETCH_MESSAGES,
  FETCH_COUNT_OF_MESSAGES,
  SET_MESSAGES_CURRENT_PAGE
} from "./actionTypes";
import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

export function fetchMessages(offset, numberOfRows) {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/userMessages/" + offset + "/" + numberOfRows)
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch({ type: FETCH_MESSAGES, userMessages: response.data });
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function fetchCountOfMessages() {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/userMessages")
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch({
          type: FETCH_COUNT_OF_MESSAGES,
          totalCount: response.data
        });
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function setMessagesCurrentPage(currentPage) {
  return { type: SET_MESSAGES_CURRENT_PAGE, currentPage };
}
