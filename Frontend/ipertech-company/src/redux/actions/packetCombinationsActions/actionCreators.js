import {
  FETCH_PACKET_COMBINATIONS,
  INSERT_PACKET_COMBINATION,
  DELETE_PACKET_COMBINATION,
} from "./actionTypes";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";
import {
  getPacketCombinations,
  postPacketCombination,
  deletePacket,
} from "../../../services/packetCombinationService";

export function fetchPacketCombinations() {
  return (dispatch) => {
    getPacketCombinations().then((data) => {
      const { packetCombinations } = data.success;
      dispatch({
        type: FETCH_PACKET_COMBINATIONS,
        packetCombinations,
      });
    });
  };
}

export function insertPacketCombination(packetCombination) {
  return (dispatch) => {
    postPacketCombination(packetCombination).then((data) => {
      if (data.error) {
        const { message } = data.error;
        addNotification({
          type: notificationTypes.ERROR,
          message,
          duration: 5000,
        });
      }
      const { newPacketCombination, message } = data.success;
      dispatch({
        type: INSERT_PACKET_COMBINATION,
        newPacketCombination,
      });

      dispatch(
        addNotification({
          type: notificationTypes.SUCCESS,
          message: message + newPacketCombination.name,
          duration: 5000,
        })
      );
    });
  };
}

export function deletePacketCombination(packetCombination) {
  return (dispatch) => {
    deletePacket(packetCombination).then((data) => {
      const { message } = data.success;
      dispatch({
        type: DELETE_PACKET_COMBINATION,
        packetCombinationId: packetCombination.packetCombinationId,
      });

      return dispatch(
        addNotification({
          type: notificationTypes.INFO,
          message,
          duration: 5000,
        })
      );
    });
  };
}
