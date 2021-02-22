import { FETCH_PROMOTIONS } from "./actionTypes";
import { getPromotions } from "../../../services/promotionService";

export function fetchPromotions() {
  return (dispatch) => {
    getPromotions().then((data) => {
      const { promotions } = data.success;

      dispatch({
        type: FETCH_PROMOTIONS,
        promotions,
      });
    });
  };
}
