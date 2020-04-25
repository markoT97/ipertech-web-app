import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

export async function getUserContracts() {
  return axios
    .get(BACKEND_URL + "api/userContracts")
    .then((response) => {
      return { success: { userContracts: response.data } };
    })
    .catch((error) => {
      //console.error(error);
    });
}

export async function postUserContract(userContract) {
  return axios
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
      return {
        success: {
          newUserContract,
          message:
            "Uspešno ste dodali novi korisnički ugovor " +
            newUserContract.userContractId,
        },
      };
    })
    .catch((error) => {});
}

export async function putUserContract(userContract) {
  return axios
    .put(BACKEND_URL + "api/userContracts", userContract, {
      headers: { "Content-Type": "application/json" },
    })
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      const updatedUserContract = response.data;
      return {
        success: {
          message:
            "Vaš paket je sada " + updatedUserContract.packetCombination.name,
        },
      };
    })
    .catch((err) => {});
}

export async function deleteUserCont(userContract) {
  return axios
    .delete(BACKEND_URL + "api/userContracts/" + userContract.userContractId)
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }

      return {
        success: {
          message: `Korisnički ugovor ${userContract.userContractId} je izbrisan`,
        },
      };
    })
    .catch((error) => {
      console.error(error);
    });
}
