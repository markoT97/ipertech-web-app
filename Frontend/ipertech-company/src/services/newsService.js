import axios from "axios";
import { BACKEND_URL } from "./../shared/constants";

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

export async function postNews(news) {
  let data = new FormData();
  data.append("notificationTypeId", "9fba9021-1a99-47f6-abc7-c132c3ac7a0e");
  data.append("title", news.title);
  data.append("content", news.content);
  data.append("image", news.image);

  return axios
    .post(BACKEND_URL + "api/notifications", data, {
      headers: { "Content-Type": "multipart/form-data" },
    })
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { data: response.data } };
    });
}

export async function deleteNew(notificationId, notificationTypeId) {
  return axios
    .delete(BACKEND_URL + "api/notifications/" + notificationId + "/" + notificationTypeId)
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }

      return { success: { message: `Novost je izbrisana` } };
    })
    .catch((error) => {
      console.error(error);
    });
}
