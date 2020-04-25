import axios from "axios";
import { BACKEND_URL } from "../shared/constants";

export async function getUserVote(pollId, userId) {
  return axios
    .get(BACKEND_URL + "api/optionsVoters/" + pollId + "/" + userId)
    .then((response) => {
      return { success: { isCurrentUserVoted: response.data } };
    })
    .catch((error) => {
      //console.error(error);
    });
}

export async function getNumberOfVoters(pollId) {
  return axios
    .get(BACKEND_URL + "api/optionsVoters/" + pollId)
    .then((response) => {
      return { success: { results: response.data } };
    })
    .catch((error) => {
      //console.error(error);
    });
}

export async function postVote(optionVoter) {
  return axios
    .post(BACKEND_URL + "api/optionsVoters", optionVoter, {
      headers: { "Content-Type": "application/json" },
    })
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return { success: { data: response.data } };
    });
}
