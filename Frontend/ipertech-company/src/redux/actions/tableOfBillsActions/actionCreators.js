import {
  SET_TABLE_OF_BILLS_VISIBILITY,
  SET_TABLE_OF_BILLS_CURRENT_PAGE
} from "./actionTypes";

export function setTableOfBillsVisibility(visibility) {
  return { type: SET_TABLE_OF_BILLS_VISIBILITY, visibility };
}

export function setTableOfBillsCurrentPage(currentPage) {
  return { type: SET_TABLE_OF_BILLS_CURRENT_PAGE, currentPage };
}
