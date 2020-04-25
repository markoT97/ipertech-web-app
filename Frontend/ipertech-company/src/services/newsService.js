import axios from "axios";
import { BACKEND_URL } from "./../redux/actions/backendServerSettings";

const numberOfNewsToShow = 3;
const notificationTypeName = "novosti";

export async function getNews() {
  return axios
    .get(
      BACKEND_URL +
        "api/notifications/" +
        notificationTypeName +
        "/notificationTypes/" +
        numberOfNewsToShow
    )
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { news: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}
