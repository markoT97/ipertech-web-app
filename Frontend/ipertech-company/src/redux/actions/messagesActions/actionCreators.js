import {
  FETCH_MESSAGES,
  FETCH_COUNT_OF_MESSAGES,
  SET_MESSAGES_CURRENT_PAGE,
  INSERT_MESSAGE,
  UPDATE_FETCHED_MESSAGES_USER_IMAGE
} from "./actionTypes";
import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";
import {
  numberOfMessagesPerPage,
  notificationTypes
} from "../../../shared/constants";
import { addNotification } from "../notificationsActions/actionCreators";

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
        //console.error(error);
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
        //console.error(error);
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
          createdAt: new Date(),
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

        const message = response.data;

        return dispatch(insertUserMessage(user, message));
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

        dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message: "Vaša poruka je dodata u ćaskanje",
            duration: 5000
          })
        );

        return dispatch({
          type: INSERT_MESSAGE,
          newUserMessage: { user, message }
        });
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function deleteMessage(messageId) {
  return dispatch => {
    axios
      .delete(BACKEND_URL + "api/messages/" + messageId)
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        dispatch(
          addNotification({
            type: notificationTypes.INFO,
            message: "Vaša poruka je izbrisana iz ćaskanja",
            duration: 5000
          })
        );

        return dispatch(fetchMessages(0, numberOfMessagesPerPage));
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function updateMessagesUserImage(userId, imageLocation) {
  return {
    type: UPDATE_FETCHED_MESSAGES_USER_IMAGE,
    userId,
    imageLocation
  };
}
