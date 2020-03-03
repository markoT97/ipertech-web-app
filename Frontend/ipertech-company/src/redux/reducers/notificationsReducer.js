import { FETCH_NOTIFICATIONS } from "../actions/actions";

const initialState = {
  notifications: []
};

function reducer(state = initialState, action) {
  switch (action.type) {
    case FETCH_NOTIFICATIONS:
      return {
        ...state,
        notifications: action.notifications
      };
    default:
      return state;
  }
}

export default reducer;
