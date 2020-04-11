import { ADD_NOTIFICATION, REMOVE_NOTIFICATION } from "./actionTypes";

export function addNotification(notification) {
  const notificationWithShowProperty = { show: true, ...notification };
  return dispatch => {
    dispatch({
      type: ADD_NOTIFICATION,
      notification: notificationWithShowProperty
    });
    setTimeout(() => {
      return dispatch(removeNotification(notificationWithShowProperty));
    }, notificationWithShowProperty.duration);
    return;
  };
}
export function removeNotification(notification) {
  return { type: REMOVE_NOTIFICATION, notification };
}
