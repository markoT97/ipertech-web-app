import {
  SET_TABLE_OF_BILLS_VISIBILITY,
  SET_TABLE_OF_BILLS_CURRENT_PAGE
} from "./../actions/tableOfBillsActions/actionTypes";

const initialTableOfBills = {
  currentPage: 1,
  visibility: false
};

function tableOfBillsReducer(tableOfBills = initialTableOfBills, action) {
  switch (action.type) {
    case SET_TABLE_OF_BILLS_VISIBILITY:
      return { ...tableOfBills, visibility: action.visibility };
    case SET_TABLE_OF_BILLS_CURRENT_PAGE:
      return { ...tableOfBills, currentPage: action.currentPage };
    default:
      return tableOfBills;
  }
}

export default tableOfBillsReducer;
