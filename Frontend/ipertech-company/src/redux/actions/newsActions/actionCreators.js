import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_NEWS } from "./actionTypes";

const numberOfNewsToShow = 3;
const notificationTypeName = "novosti";

function fetchNews() {
  return dispatch => {
    fetch(
      BACKEND_URL +
        "api/notifications/" +
        notificationTypeName +
        "/notificationTypes/" +
        numberOfNewsToShow
    )
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(news => {
        dispatch({
          type: FETCH_NEWS,
          news
        });
      })
      .catch(err => {
        console.error(err);
      });
  };
}
export default fetchNews;
