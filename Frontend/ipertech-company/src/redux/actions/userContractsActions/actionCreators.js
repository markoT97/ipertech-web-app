import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import {
  FETCH_USER_CONTRACTS,
  INSERT_USER_CONTRACT,
  DELETE_USER_CONTRACT,
} from "./actionTypes";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";

export function fetchUserContracts() {
  return (dispatch) => {
    axios
      .get(BACKEND_URL + "api/userContracts")
      .then((response) => {
        dispatch({ type: FETCH_USER_CONTRACTS, userContracts: response.data });
      })
      .catch((error) => {
        //console.error(error);
      });
  };
}

export function insertUserContract(userContract) {
  return (dispatch) => {
    axios
      .post(
        BACKEND_URL + "api/userContracts",
        {
          ...userContract,
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

        const newUserContract = response.data;

        dispatch({
          type: INSERT_USER_CONTRACT,
          newUserContract,
        });

        dispatch(
          addNotification({
            type: notificationTypes.SUCCESS,
            message:
              "Uspešno ste dodali novi korisnički ugovor " +
              newUserContract.userContractId,
            duration: 5000,
          })
        );
      })
      .catch((error) => {});
  };
}

export function deleteUserContract(userContract) {
  return (dispatch) => {
    axios
      .delete(BACKEND_URL + "api/userContracts/" + userContract.userContractId)
      .then((response) => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        dispatch({
          type: DELETE_USER_CONTRACT,
          userContractId: userContract.userContractId,
        });

        return dispatch(
          addNotification({
            type: notificationTypes.INFO,
            message: `Kombinacija paketa ${userContract.userContractId} je izbrisana`,
            duration: 5000,
          })
        );
      })
      .catch((error) => {
        console.error(error);
      });
  };
}
