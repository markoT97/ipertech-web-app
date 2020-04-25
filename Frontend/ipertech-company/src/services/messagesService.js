import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

export async function getMessages(offset, numberOfRows) {
  return axios
    .get(BACKEND_URL + "api/userMessages/" + offset + "/" + numberOfRows)
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { userMessages: response.data } };
    })
    .catch((error) => {
      return { error: { message: "Došlo je do greške" } };
    });
}

export async function getMessagesCount() {
  return axios
    .get(BACKEND_URL + "api/userMessages")
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { totalCount: response.data } };
    })
    .catch((error) => {
      return { error: { message: "Došlo je do greške" } };
    });
}

export async function postMessage(message) {
  return axios
    .post(
      BACKEND_URL + "api/messages",
      {
        ...message,
        createdAt: new Date(),
        category: "Utisci",
      },
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }

      return { success: { insertedMessage: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}

export async function deleteMessag(messageId) {
  return axios
    .delete(BACKEND_URL + "api/messages/" + messageId)
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }

      return { success: { message: "Vaša poruka je izbrisana iz ćaskanja" } };
    })
    .catch((error) => {
      console.error(error);
    });
}
