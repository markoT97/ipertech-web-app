import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

export async function getLatestPoll() {
  return axios
    .get(BACKEND_URL + "api/polls")
    .then((response) => {
      return { success: { poll: response.data } };
    })
    .catch((error) => {
      //console.error(error);
    });
}
