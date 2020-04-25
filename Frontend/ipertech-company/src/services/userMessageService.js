import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

export async function postUserMessage(user, message) {
  return axios
    .post(
      BACKEND_URL + "api/userMessages",
      {
        user,
        message,
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
      return { success: { message: "Vaša poruka je dodata u ćaskanje" } };
    })
    .catch((error) => {
      return { error: { message: "Došlo je do greške" } };
    });
}
