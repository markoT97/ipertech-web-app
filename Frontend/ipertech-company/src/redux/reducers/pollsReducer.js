import { FETCH_THE_LATEST_POLL } from "./../actions/pollActions/actionTypes";

const initialPoll = {
  pollId: "",
  question: "",
  numberOfVoters: 0,
  options: []
};

function pollReducer(poll = initialPoll, action) {
  switch (action.type) {
    case FETCH_THE_LATEST_POLL:
      return action.polls;
    default:
      return poll;
  }
}

export default pollReducer;
