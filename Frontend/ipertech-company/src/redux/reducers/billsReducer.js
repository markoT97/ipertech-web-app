import {
  FETCH_BILLS,
  FETCH_COUNT_OF_BILLS
} from "./../actions/billsActions/actionTypes";

const initialBills = {
  data: [],
  totalCount: 0
};

function billsReducer(bills = initialBills, action) {
  switch (action.type) {
    case FETCH_BILLS:
      return { ...bills, data: action.bills };
    case FETCH_COUNT_OF_BILLS:
      return { ...bills, totalCount: action.totalCount };
    default:
      return bills;
  }
}

export default billsReducer;
