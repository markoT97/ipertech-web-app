import axios from "axios";
import { BACKEND_URL } from "../redux/actions/backendServerSettings";

export async function getTvPackets() {
  return axios
    .get(BACKEND_URL + "api/tvPackets")
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { tvPackets: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}
