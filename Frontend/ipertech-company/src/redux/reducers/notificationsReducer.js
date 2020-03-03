import { FETCH_NOTIFICATIONS } from "../actions/notificationsActions/actionTypes";

function reducer(notifications = [], action) {
  switch (action.type) {
    case FETCH_NOTIFICATIONS:
      return {
        ...notifications,
        notifications: action.notifications
      };
    default:
      return notifications;
  }
}

export default reducer;
