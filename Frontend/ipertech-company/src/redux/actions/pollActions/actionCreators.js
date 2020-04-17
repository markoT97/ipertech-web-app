import {
  FETCH_THE_LATEST_POLL,
  CHECK_IS_USER_VOTED_ON_POLL,
  FETCH_NUMBER_OF_VOTERS_FOR_POLL_OPTIONS
} from "./actionTypes";
import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

export function fetchLatestPoll(userId) {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/polls")
      .then(response => {
        const pollId = response.data.pollId;
        dispatch(checkIsUserVotedOnPoll(pollId, userId));
        dispatch(fetchNumberOfVotersForPollOptionsByPollId(pollId));
        dispatch({ type: FETCH_THE_LATEST_POLL, poll: response.data });
      })
      .catch(error => {
        //console.error(error);
      });
  };
}

export function voteInPoll(optionVoter) {
  return dispatch => {
    axios
      .post(BACKEND_URL + "api/optionsVoters", optionVoter, {
        headers: { "Content-Type": "application/json" }
      })
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }
        dispatch(
          checkIsUserVotedOnPoll(optionVoter.pollId, optionVoter.userId)
        );
      });
  };
}

export function checkIsUserVotedOnPoll(pollId, userId) {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/optionsVoters/" + pollId + "/" + userId)
      .then(response => {
        dispatch({
          type: CHECK_IS_USER_VOTED_ON_POLL,
          isCurrentUserVoted: response.data
        });
      })
      .catch(error => {
        //console.error(error);
      });
  };
}

export function fetchNumberOfVotersForPollOptionsByPollId(pollId) {
  return dispatch => {
    axios
      .get(BACKEND_URL + "api/optionsVoters/" + pollId)
      .then(response => {
        dispatch({
          type: FETCH_NUMBER_OF_VOTERS_FOR_POLL_OPTIONS,
          results: response.data
        });
      })
      .catch(error => {
        //console.error(error);
      });
  };
}
