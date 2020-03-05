import { FETCH_PROMOTIONS } from "../actions/promotionsActions/actionTypes";

function promotionsReducer(promotions = [], action) {
  switch (action.type) {
    case FETCH_PROMOTIONS:
      return action.promotions;
    default:
      return promotions;
  }
}

export default promotionsReducer;
