export const FETCH_NOTIFICATIONS = "FETCH_NOTIFICATIONS";

function fetchNotifications() {
  return {
    type: FETCH_NOTIFICATIONS,
    notifications: [{ title: "First Title", content: "First Content" }]
  };
}
export default fetchNotifications;
