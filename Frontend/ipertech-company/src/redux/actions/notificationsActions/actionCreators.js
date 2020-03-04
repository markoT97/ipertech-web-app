import fetch from "cross-fetch";
import { BACKEND_URL } from "./../backendServerSettings";

import { FETCH_NOTIFICATIONS } from "./actionTypes";

const numberOfNotificationsToShow = 3;

function fetchNotifications(notificationTypeName) {
  return dispatch => {
    fetch(
      BACKEND_URL +
        "api/notifications/" +
        notificationTypeName +
        "/notificationTypes/" +
        numberOfNotificationsToShow
    )
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(notifications => {
        dispatch({
          type: FETCH_NOTIFICATIONS,
          notifications
        });
      })
      .catch(err => {
        console.error(err);
      });
  };
}
export default fetchNotifications;
