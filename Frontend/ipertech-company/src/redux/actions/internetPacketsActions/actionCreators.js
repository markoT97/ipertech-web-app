import { FETCH_INTERNET_PACKETS } from "./actionTypes";
import { getInternetPackets } from "../../../services/internetPacketService";

function fetchInternetPackets() {
  return (dispatch) => {
    getInternetPackets().then((data) => {
      const { internetPackets } = data.success;
      dispatch({
        type: FETCH_INTERNET_PACKETS,
        internetPackets,
      });
    });
  };
}
export default fetchInternetPackets;
