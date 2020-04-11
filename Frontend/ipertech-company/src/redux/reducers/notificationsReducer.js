import {
  ADD_NOTIFICATION,
  REMOVE_NOTIFICATION
} from "../actions/notificationsActions/actionTypes";

function notificationsReducer(notifications = [], action) {
  switch (action.type) {
    case ADD_NOTIFICATION:
      const { notification } = action;
      return [notification, ...notifications];
    case REMOVE_NOTIFICATION:
      for (let i = 0; i < notifications.length; i++) {
        if (notifications[i] === action.notification) {
          notifications[i].show = false;
        }
      }
      return [...notifications];
    default:
      return notifications;
  }
}

export default notificationsReducer;
