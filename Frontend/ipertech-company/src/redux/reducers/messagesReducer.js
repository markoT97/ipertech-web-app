import {
  FETCH_MESSAGES,
  FETCH_COUNT_OF_MESSAGES
} from "./../actions/messagesActions/actionTypes";

const initialUserMessages = {
  data: [],
  totalCount: 0
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
    default:
      return userMessages;
  }
}