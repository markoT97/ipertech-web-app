import axios from "axios";
import { BACKEND_URL } from "../redux/actions/backendServerSettings";

export async function getPacketCombinations() {
  return axios
    .get(BACKEND_URL + "api/packetCombinations")
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { packetCombinations: response.data } };
    })
    .catch((error) => {
      console.error(error);
    });
}

export async function getPacketCombination(packetCombination) {
  try {
    const response = await axios.get(
      BACKEND_URL +
        "api/packetCombinations/ids?internetPacketId=" +
        packetCombination.internetPacketId +
        (packetCombination.tvPacketId
          ? `&${"tvPacketId=" + packetCombination.tvPacketId}`
          : "") +
        (packetCombination.phonePacketId
          ? `&${"phonePacketId=" + packetCombination.phonePacketId}`
          : "")
    );
    if (response.status >= 400) {
      throw new Error("Bad response from server");
    }
    const foundedPacketCombination = response.data;
    if (!foundedPacketCombination) {
      return {
        error: {
          message: "Izabrana kombinacija paketa trenutno ne postoji u ponudi",
        },
      };
    }
    return {
      success: { foundedPacketCombination },
    };
  } catch (error) {
    console.error(error);
  }
}

export async function postPacketCombination(packetCombination) {
  return axios
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

      return {
        success: {
          newPacketCombination: response.data,
          message: "Uspešno ste dodali novu kombinaciju paketa ",
        },
      };
    })
    .catch((error) => {
      return { error: { message: "Ova kombinacija paketa već postoji" } };
    });
}

export async function deletePacket(packetCombination) {
  return axios
    .delete(
      BACKEND_URL +
        "api/packetCombinations/" +
        packetCombination.packetCombinationId
    )
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }

      return {
        success: {
          message: `Kombinacija paketa ${packetCombination.name} je izbrisana`,
        },
      };
    })
    .catch((error) => {
      console.error(error);
    });
}
