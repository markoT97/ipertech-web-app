import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import { FETCH_PROMOTIONS } from "./actionTypes";

const numberOfPromotionsToShow = 3;
const notificationTypeName = "promocije";

function fetchPromotions() {
  return dispatch => {
    fetch(
      BACKEND_URL +
        "api/notifications/" +
        notificationTypeName +
        "/notificationTypes/" +
        numberOfPromotionsToShow
    )
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(promotions => {
        dispatch({
          type: FETCH_PROMOTIONS,
          promotions
        });
      })
      .catch(err => {
        console.error(err);
      });
  };
}
export default fetchPromotions;
