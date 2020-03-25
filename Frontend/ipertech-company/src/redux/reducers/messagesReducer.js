import {
  FETCH_MESSAGES,
  FETCH_COUNT_OF_MESSAGES,
  SET_MESSAGES_CURRENT_PAGE
} from "./../actions/messagesActions/actionTypes";

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
    default:
      return userMessages;
  }
}
