import {
  FETCH_BILLS,
  FETCH_COUNT_OF_BILLS,
  FETCH_SELECTED_BILL,
} from "./actionTypes";
import { getBills, getBillsCount } from "../../../services/billService";

export function fetchBills(userContractId, offset, numberOfRows) {
  return (dispatch) => {
    getBills(userContractId, offset, numberOfRows).then((data) => {
      const { bills } = data.success;
      return dispatch({ type: FETCH_BILLS, bills });
    });
  };
}

export function fetchCountOfBills(userContractId) {
  return (dispatch) => {
    getBillsCount(userContractId).then((data) => {
      const { totalCount } = data.success;
      return dispatch({
        type: FETCH_COUNT_OF_BILLS,
        totalCount,
      });
    });
  };
}

export function fetchSelectedBill(selectedBill) {
  return { type: FETCH_SELECTED_BILL, selectedBill };
}
