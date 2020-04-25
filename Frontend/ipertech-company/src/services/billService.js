import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

export async function getBills(userContractId, offset, numberOfRows) {
  return axios
    .get(
      BACKEND_URL +
        "api/bills/" +
        userContractId +
        "/" +
        offset +
        "/" +
        numberOfRows
    )
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { bills: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}

export async function getBillsCount(userContractId) {
  return axios
    .get(BACKEND_URL + "api/bills/" + userContractId)
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { totalCount: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}
