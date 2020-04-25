import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

const numberOfPromotionsToShow = 3;
const notificationTypeName = "promocije";

export async function getPromotions() {
  return axios
    .get(
      BACKEND_URL +
        "api/notifications/" +
        notificationTypeName +
        "/notificationTypes/" +
        numberOfPromotionsToShow
    )
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { promotions: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}
