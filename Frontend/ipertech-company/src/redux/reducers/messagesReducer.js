import {
  FETCH_MESSAGES,
  FETCH_COUNT_OF_MESSAGES,
  SET_MESSAGES_CURRENT_PAGE,
  INSERT_MESSAGE,
  UPDATE_FETCHED_MESSAGES_USER_IMAGE
} from "./../actions/messagesActions/actionTypes";
import { numberOfMessagesPerPage } from "../../shared/constants";

const initialUserMessages = {
  data: [],
  totalCount: 0,
  currentPage: 1
};

export default function messagesReducer(
  userMessages = initialUserMessages,
  action
) {
  switch (action.type) {
    case FETCH_MESSAGES:
      return { ...userMessages, data: action.userMessages };
    case FETCH_COUNT_OF_MESSAGES:
      return { ...userMessages, totalCount: action.totalCount };
    case SET_MESSAGES_CURRENT_PAGE:
      return { ...userMessages, currentPage: action.currentPage };
    case INSERT_MESSAGE:
      userMessages.data.unshift(action.newUserMessage);
      return {
        ...userMessages,
        data: userMessages.data.slice(0, numberOfMessagesPerPage)
      };
    case UPDATE_FETCHED_MESSAGES_USER_IMAGE:
      let updatedMessages = [];
      const messages = userMessages.data;
      for (let i = 0; i < messages.length; i++) {
        const message = messages[i];

        message.user.userId === action.userId
          ? updatedMessages.push({
              ...message,
              user: {
                ...message.user,
                imageLocation: action.imageLocation
              }
            })
          : updatedMessages.push(message);
      }

      return {
        ...userMessages,
        data: updatedMessages
      };
    default:
      return userMessages;
  }
}
