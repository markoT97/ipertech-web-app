import {
  FETCH_THE_LATEST_POLL,
  CHECK_IS_USER_VOTED_ON_POLL,
  FETCH_NUMBER_OF_VOTERS_FOR_POLL_OPTIONS,
  INCREASE_NUMBER_OF_VOTERS_FOR_POLL,
} from "./../actions/pollActions/actionTypes";

const initialPoll = {
  data: {
    pollId: "",
    question: "",
    numberOfVoters: 0,
    options: [],
  },
  results: [],
  isCurrentUserVoted: false,
};

function pollReducer(poll = initialPoll, action) {
  switch (action.type) {
    case FETCH_THE_LATEST_POLL:
      return { ...poll, data: action.poll };
    case CHECK_IS_USER_VOTED_ON_POLL:
      return { ...poll, isCurrentUserVoted: action.isCurrentUserVoted };
    case FETCH_NUMBER_OF_VOTERS_FOR_POLL_OPTIONS:
      return { ...poll, results: action.results };
    case INCREASE_NUMBER_OF_VOTERS_FOR_POLL:
      return {
        ...poll,
        data: { ...poll.data, numberOfVoters: poll.data.numberOfVoters + 1 },
      };
    default:
      return poll;
  }
}

export default pollReducer;
