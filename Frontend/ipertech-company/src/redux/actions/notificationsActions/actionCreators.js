import { FETCH_NOTIFICATIONS } from "./actionTypes";

function fetchNotifications() {
  return {
    type: FETCH_NOTIFICATIONS,
    notifications: [{ title: "First Title", content: "First Content" }]
  };
}
export default fetchNotifications;
