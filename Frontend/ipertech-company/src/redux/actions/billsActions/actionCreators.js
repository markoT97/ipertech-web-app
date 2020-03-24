import {
  FETCH_BILLS,
  FETCH_COUNT_OF_BILLS,
  FETCH_SELECTED_BILL
} from "./actionTypes";
import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

export function fetchBills(userContractId, offset, numberOfRows) {
  return dispatch => {
    axios
      .get(
        BACKEND_URL +
          "api/bills/" +
          userContractId +
          "/" +
          offset +
          "/" +
          numberOfRows
      )
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch({ type: FETCH_BILLS, bills: response.data });
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function fetchCountOfBills(userContractId) {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/bills/" + userContractId)
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch({
          type: FETCH_COUNT_OF_BILLS,
          totalCount: response.data
        });
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function fetchSelectedBill(selectedBill) {
  return { type: FETCH_SELECTED_BILL, selectedBill };
}
