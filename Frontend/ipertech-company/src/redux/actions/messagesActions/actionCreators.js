import {
  FETCH_MESSAGES,
  FETCH_COUNT_OF_MESSAGES,
  SET_MESSAGES_CURRENT_PAGE,
  INSERT_MESSAGE,
  UPDATE_FETCHED_MESSAGES_USER_IMAGE,
} from "./actionTypes";
import {
  numberOfMessagesPerPage,
  notificationTypes,
} from "../../../shared/constants";
import { addNotification } from "../notificationsActions/actionCreators";
import {
  getMessages,
  getMessagesCount,
  postMessage,
  deleteMessag,
} from "../../../services/messagesService";
import { postUserMessage } from "../../../services/userMessageService";

export function fetchMessages(offset, numberOfRows) {
  return (dispatch) => {
    getMessages(offset, numberOfRows).then((data) => {
      if (data.error) {
        const { message } = data.error;
        return dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: message,
            duration: 5000,
          })
        );
      }
      const { userMessages } = data.success;
      return dispatch({ type: FETCH_MESSAGES, userMessages });
    });
  };
}

export function fetchCountOfMessages() {
  return (dispatch) => {
    getMessagesCount().then((data) => {
      if (data.error) {
        const { message } = data.error;
        return dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: message,
            duration: 5000,
          })
        );
      }
      const { totalCount } = data.success;
      return dispatch({
        type: FETCH_COUNT_OF_MESSAGES,
        totalCount,
      });
    });
  };
}

export function setMessagesCurrentPage(currentPage) {
  return { type: SET_MESSAGES_CURRENT_PAGE, currentPage };
}

export function insertMessage(user, message) {
  return (dispatch) => {
    postMessage(message).then((data) => {
      const { insertedMessage } = data.success;

      postUserMessage(user, insertedMessage).then((data) => {
        if (data.error) {
          const { message: responseMessage } = data.error;
          return dispatch(
            addNotification({
              type: notificationTypes.ERROR,
              message: responseMessage,
              duration: 5000,
            })
          );
        }

        const { message: responseMessage } = data.success;
        dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message: responseMessage,
            duration: 5000,
          })
        );

        return dispatch({
          type: INSERT_MESSAGE,
          newUserMessage: { user, message: insertedMessage },
        });
      });
    });
  };
}

export function deleteMessage(messageId) {
  return (dispatch) => {
    deleteMessag(messageId).then((data) => {
      const { message } = data.success;
      dispatch(
        addNotification({
          type: notificationTypes.INFO,
          message: message,
          duration: 5000,
        })
      );

      return dispatch(fetchMessages(0, numberOfMessagesPerPage));
    });
  };
}

export function updateMessagesUserImage(userId, imageLocation) {
  return {
    type: UPDATE_FETCHED_MESSAGES_USER_IMAGE,
    userId,
    imageLocation,
  };
}
