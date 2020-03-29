import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import {
  FETCH_USER_BY_ID,
  INSERT_USER,
  FETCH_PACKET_COMBINATION_BY_PACKET_IDS
} from "./actionTypes";

export function fetchUserById(id) {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/users/" + id)
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        return dispatch({ type: FETCH_USER_BY_ID, user: response.data });
      })
      .catch(error => {
        console.error(error);
      });
  };
}

export function insertUser(user) {
  return dispatch => {
    axios
      .post(
        BACKEND_URL + "api/users",
        { ...user, role: "User" },
        {
          headers: { "Content-Type": "application/json" }
        }
      )
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        const user = response.data;
        console.log("Inserted user:");
        console.log(user);
        return dispatch({ type: INSERT_USER, insertedUser: user });
      });
  };
}

export function fetchPacketCombinationByInternetAndTvAndPhonePacketId(
  packetCombination,
  userContract
) {
  return dispatch => {
    axios
      .get(
        BACKEND_URL +
          "api/packetCombinations/ids?internetPacketId=" +
          packetCombination.internetPacketId +
          (packetCombination.tvPacketId
            ? `&${"tvPacketId=" + packetCombination.tvPacketId}`
            : "") +
          (packetCombination.phonePacketId
            ? `&${"phonePacketId=" + packetCombination.phonePacketId}`
            : "")
      )
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        const foundedPacketCombination = response.data;

        if (!foundedPacketCombination) {
          throw new Error("Packet combination does not exists");
        }

        console.log(foundedPacketCombination);

        dispatch(
          updateUserContract({
            ...userContract,
            packetCombination: foundedPacketCombination
          })
        );

        return dispatch({
          type: FETCH_PACKET_COMBINATION_BY_PACKET_IDS,
          packetCombination: foundedPacketCombination
        });
      })
      .catch(error => {
        console.error(error);
      });
  };
}

function updateUserContract(userContract) {
  return dispatch => {
    axios
      .put(BACKEND_URL + "api/userContracts", userContract, {
        headers: { "Content-Type": "application/json" }
      })
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        const updatedUserContract = response.data;
        console.log("Updated user contract: ");
        console.log(updatedUserContract);
        //return dispatch({ type: CHANGE_TV_PACKET, insertedUser: user });
      });
  };
}
