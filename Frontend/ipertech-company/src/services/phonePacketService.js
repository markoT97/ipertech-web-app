import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

export async function getPhonePackets() {
  return axios
    .get(BACKEND_URL + "api/phonePackets")
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { phonePackets: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}
