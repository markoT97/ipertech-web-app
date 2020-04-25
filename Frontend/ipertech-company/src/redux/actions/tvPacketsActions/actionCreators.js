import { FETCH_TV_PACKETS } from "./actionTypes";
import { getTvPackets } from "../../../services/tvPacketService";

function fetchTvPackets() {
  return (dispatch) => {
    getTvPackets().then((data) => {
      const { tvPackets } = data.success;

      dispatch({
        type: FETCH_TV_PACKETS,
        tvPackets,
      });
    });
  };
}
export default fetchTvPackets;
