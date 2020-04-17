import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import {
  FETCH_USER_BY_ID,
  INSERT_USER,
  FETCH_PACKET_COMBINATION_BY_PACKET_IDS,
  UPDATE_USER_IMAGE
} from "./actionTypes";
import { updateMessagesUserImage } from "./../messagesActions/actionCreators";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";

export function fetchUserByContractId(id) {
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
        //console.error(error);
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

        dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message: "Uspešno ste se registrovali",
            duration: 5000
          })
        );

        return dispatch({ type: INSERT_USER, insertedUser: user });
      })
      .catch(err => {
        if (!err.response) {
          return dispatch(
            addNotification({
              type: notificationTypes.ERROR,
              message: "Imejl se već koristi",
              duration: 5000
            })
          );
        }

        const { status } = err.response;
        if (status === 404) {
          return dispatch(
            addNotification({
              type: notificationTypes.ERROR,
              message: "Broj ugovora je nevažeći",
              duration: 5000
            })
          );
        } else if (status === 409)
          return dispatch(
            addNotification({
              type: notificationTypes.ERROR,
              message: "Već postoji Korisnik sa tim brojem ugovora",
              duration: 5000
            })
          );
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
          return dispatch(
            addNotification({
              type: notificationTypes.ERROR,
              message:
                "Izabrana kombinacija paketa trenutno ne postoji u ponudi",
              duration: 5000
            })
          );
        }

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
        return dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message:
              "Vaš paket je sada " + updatedUserContract.packetCombination.name,
            duration: 5000
          })
        );
      });
  };
}

export function updateUserImage(userImage) {
  return dispatch => {
    let data = new FormData();
    data.append("userId", userImage.userId);
    data.append("image", userImage.image);

    axios
      .patch(BACKEND_URL + "api/users", data, {
        headers: { "Content-Type": "multipart/form-data" }
      })
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        const imageLocation = response.data + "?" + Date.now();

        dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message: "Vaša slika je uspešno ažurirana",
            duration: 5000
          })
        );

        dispatch(updateMessagesUserImage(userImage.userId, imageLocation));
        return dispatch({
          type: UPDATE_USER_IMAGE,
          imageLocation
        });
      });
  };
}

export function updateUserPassword(newPassword) {
  return dispatch => {
    axios
      .patch(BACKEND_URL + "api/users/password", newPassword, {
        headers: { "Content-Type": "application/json" }
      })
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        return dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message: "Vaša lozinka je uspešno ažurirana",
            duration: 5000
          })
        );
      });
  };
}
