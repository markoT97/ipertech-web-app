import {
  FETCH_USER_BY_ID,
  INSERT_USER,
  FETCH_PACKET_COMBINATION_BY_PACKET_IDS,
  UPDATE_USER_IMAGE,
} from "./actionTypes";
import { updateMessagesUserImage } from "./../messagesActions/actionCreators";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";
import {
  getUserById,
  postUser,
  patchUserImage,
  patchUserPassword,
} from "../../../services/userService";
import { getPacketCombination } from "../../../services/packetCombinationService";
import { putUserContract } from "../../../services/userContractService";

export function fetchUserByContractId(id) {
  return (dispatch) => {
    getUserById(id).then((user) => {
      return dispatch({
        type: FETCH_USER_BY_ID,
        user,
      });
    });
  };
}

export function insertUser(user) {
  return (dispatch) => {
    postUser(user).then((data) => {
      if (data.error) {
        return dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: data.error,
            duration: 5000,
          })
        );
      }
      dispatch(
        addNotification({
          type: notificationTypes.SUCCESS,
          message: data.success.message,
          duration: 5000,
        })
      );
      return dispatch({
        type: INSERT_USER,
        insertedUser: data.success.insertedUser,
      });
    });
  };
}

export function fetchPacketCombinationByInternetAndTvAndPhonePacketId(
  packetCombination,
  userContract
) {
  return (dispatch) => {
    getPacketCombination(packetCombination).then((data) => {
      if (data.error) {
        return dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: data.error.message,
            duration: 5000,
          })
        );
      }

      const { foundedPacketCombination } = data.success;

      dispatch(
        updateUserContract({
          ...userContract,
          packetCombination: foundedPacketCombination,
        })
      );

      return dispatch({
        type: FETCH_PACKET_COMBINATION_BY_PACKET_IDS,
        packetCombination: foundedPacketCombination,
      });
    });
  };
}

function updateUserContract(userContract) {
  return (dispatch) => {
    putUserContract(userContract).then((data) => {
      const { message } = data.success;
      return dispatch(
        addNotification({
          type: notificationTypes.SUCCESS,
          message,
          duration: 5000,
        })
      );
    });
  };
}

export function updateUserImage(userImage) {
  return (dispatch) => {
    patchUserImage(userImage).then((data) => {
      const { imageLocation, message } = data.success;
      dispatch(
        addNotification({
          type: notificationTypes.SUCCESS,
          message,
          duration: 5000,
        })
      );

      dispatch(updateMessagesUserImage(userImage.userId, imageLocation));
      return dispatch({
        type: UPDATE_USER_IMAGE,
        imageLocation,
      });
    });
  };
}

export function updateUserPassword(newPassword) {
  return (dispatch) => {
    patchUserPassword(newPassword).then((data) => {
      const { message } = data.success;
      return dispatch(
        addNotification({
          type: notificationTypes.SUCCESS,
          message,
          duration: 5000,
        })
      );
    });
  };
}
