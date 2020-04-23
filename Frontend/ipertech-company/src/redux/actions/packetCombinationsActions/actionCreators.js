import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import {
  FETCH_PACKET_COMBINATIONS,
  INSERT_PACKET_COMBINATION,
  DELETE_PACKET_COMBINATION,
} from "./actionTypes";
import axios from "axios";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";

export function fetchPacketCombinations() {
  return (dispatch) => {
    fetch(BACKEND_URL + "api/packetCombinations")
      .then((res) => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then((packetCombinations) => {
        dispatch({
          type: FETCH_PACKET_COMBINATIONS,
          packetCombinations,
        });
      })
      .catch((err) => {
        //console.error(err);
      });
  };
}

export function insertPacketCombination(packetCombination) {
  return (dispatch) => {
    axios
      .post(
        BACKEND_URL + "api/packetCombinations",
        {
          ...packetCombination,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
      .then((response) => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        const newPacketCombination = response.data;

        dispatch({
          type: INSERT_PACKET_COMBINATION,
          newPacketCombination,
        });

        dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message:
              "Uspešno ste dodali novu kombinaciju paketa " +
              newPacketCombination.name,
            duration: 5000,
          })
        );
      })
      .catch((error) => {
        addNotification({
          type: notificationTypes.ERROR,
          message: "Ova kombinacija paketa već postoji",
          duration: 5000,
        });
      });
  };
}

export function deletePacketCombination(packetCombination) {
  return (dispatch) => {
    axios
      .delete(
        BACKEND_URL +
          "api/packetCombinations/" +
          packetCombination.packetCombinationId
      )
      .then((response) => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        dispatch({
          type: DELETE_PACKET_COMBINATION,
          packetCombinationId: packetCombination.packetCombinationId,
        });

        return dispatch(
          addNotification({
            type: notificationTypes.INFO,
            message: `Kombinacija paketa ${packetCombination.name} je izbrisana`,
            duration: 5000,
          })
        );
      })
      .catch((error) => {
        console.error(error);
      });
  };
}
