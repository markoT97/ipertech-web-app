import {
  FETCH_THE_LATEST_POLL,
  CHECK_IS_USER_VOTED_ON_POLL,
  FETCH_NUMBER_OF_VOTERS_FOR_POLL_OPTIONS,
  INCREASE_NUMBER_OF_VOTERS_FOR_POLL,
} from "./actionTypes";
import { getLatestPoll } from "../../../services/pollService";
import {
  postVote,
  getUserVote,
  getNumberOfVoters,
} from "../../../services/optionVoterService";

export function fetchLatestPoll(userId) {
  return (dispatch) => {
    getLatestPoll().then((data) => {
      const { poll } = data.success;

      dispatch(checkIsUserVotedOnPoll(poll.pollId, userId));
      dispatch(fetchNumberOfVotersForPollOptionsByPollId(poll.pollId));
      dispatch({ type: FETCH_THE_LATEST_POLL, poll });
    });
  };
}

export function voteInPoll(optionVoter) {
  return (dispatch) => {
    postVote(optionVoter).then((data) => {
      dispatch(checkIsUserVotedOnPoll(optionVoter.pollId, optionVoter.userId));
      dispatch(fetchNumberOfVotersForPollOptionsByPollId(optionVoter.pollId));
      dispatch(increaseNumberOfVoters(optionVoter.pollId));
    });
  };
}

export function checkIsUserVotedOnPoll(pollId, userId) {
  return (dispatch) => {
    getUserVote(pollId, userId).then((data) => {
      const { isCurrentUserVoted } = data.success;

      dispatch({
        type: CHECK_IS_USER_VOTED_ON_POLL,
        isCurrentUserVoted,
      });
    });
  };
}

export function fetchNumberOfVotersForPollOptionsByPollId(pollId) {
  return (dispatch) => {
    getNumberOfVoters(pollId).then((data) => {
      const { results } = data.success;

      dispatch({
        type: FETCH_NUMBER_OF_VOTERS_FOR_POLL_OPTIONS,
        results,
      });
    });
  };
}

export function increaseNumberOfVoters(pollId) {
  return {
    type: INCREASE_NUMBER_OF_VOTERS_FOR_POLL,
    pollId,
  };
}
