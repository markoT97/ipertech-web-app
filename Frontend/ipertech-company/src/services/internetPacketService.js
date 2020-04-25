import axios from "axios";
import { BACKEND_URL } from "../redux/actions/backendServerSettings";

export async function getInternetPackets() {
  return axios
    .get(BACKEND_URL + "api/internetPackets")
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { internetPackets: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}
