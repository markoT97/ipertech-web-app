import {
  FETCH_BILLS,
  FETCH_COUNT_OF_BILLS,
  FETCH_SELECTED_BILL
} from "./../actions/billsActions/actionTypes";

const initialBills = {
  data: [],
  totalCount: 0,
  selectedBill: {}
};

function billsReducer(bills = initialBills, action) {
  switch (action.type) {
    case FETCH_BILLS:
      return { ...bills, data: action.bills };
    case FETCH_COUNT_OF_BILLS:
      return { ...bills, totalCount: action.totalCount };
    case FETCH_SELECTED_BILL:
      return { ...bills, selectedBill: action.selectedBill };
    default:
      return bills;
  }
}

export default billsReducer;
