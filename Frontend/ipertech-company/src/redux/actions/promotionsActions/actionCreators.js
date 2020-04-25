import { FETCH_PROMOTIONS } from "./actionTypes";
import { getPromotions } from "../../../services/promotionService";

function fetchPromotions() {
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
export default fetchPromotions;
