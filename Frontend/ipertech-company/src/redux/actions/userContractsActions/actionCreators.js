import {
  FETCH_USER_CONTRACTS,
  INSERT_USER_CONTRACT,
  DELETE_USER_CONTRACT,
} from "./actionTypes";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";
import {
  getUserContracts,
  postUserContract,
  deleteUserCont,
} from "../../../services/userContractService";

export function fetchUserContracts() {
  return (dispatch) => {
    getUserContracts().then((data) => {
      const { userContracts } = data.success;

      dispatch({ type: FETCH_USER_CONTRACTS, userContracts });
    });
  };
}

export function insertUserContract(userContract) {
  return (dispatch) => {
    postUserContract(userContract).then((data) => {
      const { newUserContract, message } = data.success;

      dispatch({
        type: INSERT_USER_CONTRACT,
        newUserContract,
      });

      dispatch(
        addNotification({
          type: notificationTypes.SUCCESS,
          message,
          duration: 5000,
        })
      );
    });
  };
}

export function deleteUserContract(userContract) {
  return (dispatch) => {
    deleteUserCont(userContract).then((data) => {
      const { message } = data.success;

      dispatch({
        type: DELETE_USER_CONTRACT,
        userContractId: userContract.userContractId,
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
