import { FETCH_PHONE_PACKETS } from "./actionTypes";
import { getPhonePackets } from "../../../services/phonePacketService";

function fetchPhonePackets() {
  return (dispatch) => {
    getPhonePackets().then((data) => {
      const { phonePackets } = data.success;

      return dispatch({
        type: FETCH_PHONE_PACKETS,
        phonePackets,
      });
    });
  };
}
export default fetchPhonePackets;
