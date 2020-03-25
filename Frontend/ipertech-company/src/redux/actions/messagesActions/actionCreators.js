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

export function insertMessage(user, message) {
  return dispatch => {
    axios
      .post(
        BACKEND_URL + "api/messages",
        {
          ...message,
          createdAt: new Date().toLocaleDateString(),
          category: "Utisci"
        },
        {
          headers: {
            "Content-Type": "application/json"
          }
        }
      )
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        const { messageId } = response.data;

        return dispatch(insertUserMessage(user, { messageId }));
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function insertUserMessage(user, message) {
  return dispatch => {
    axios
      .post(
        BACKEND_URL + "api/userMessages",
        {
          user,
          message
        },
        {
          headers: {
            "Content-Type": "application/json"
          }
        }
      )
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch(fetchCountOfMessages());
      })
      .catch(error => {
        console.error(error);
      });
  };
}
