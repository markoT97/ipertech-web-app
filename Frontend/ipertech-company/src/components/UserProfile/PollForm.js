import React, { Component } from "react";
import { Table, Form, Button, ProgressBar } from "react-bootstrap";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  fetchLatestPoll,
  voteInPoll,
  checkIsUserVotedOnPoll
} from "./../../redux/actions/pollActions/actionCreators";

export class PollForm extends Component {
  state = {
    selectedPollOption: ""
  };
  componentDidMount() {
    this.props.fetchLatestPoll(this.props.auth.user.userId);
  }
  handleChangeInputValue = e => {
    const { target } = e;
    this.setState({ [target.name]: target.value });
  };
  render() {
    const { poll, auth } = this.props;

    const pollResults = (
      <Table>
        <thead>
          <tr>
            <th>{poll.data.question}</th>
          </tr>
        </thead>
        <tbody>
          {poll.data.options.map((o, i) => {
            return poll.results.map((r, i) => {
              const percentageOfVoters = Math.round(
                (r.numberOfVoters / poll.data.numberOfVoters) * 100
              );
              return (
                r.pollOptionId === o.pollOptionId && (
                  <tr key={i}>
                    <td>
                      <ProgressBar
                        key={i}
                        now={percentageOfVoters}
                        variant={
                          percentageOfVoters <= 20
                            ? "danger"
                            : percentageOfVoters <= 50
                            ? "warning"
                            : "success"
                        }
                        label={o.answerText + ` (${percentageOfVoters}%)`}
                        style={{
                          height: 20 / poll.results.length + "em"
                        }}
                      />
                    </td>
                  </tr>
                )
              );
            });
          })}
          <tr>
            <td></td>
            <td></td>
          </tr>
        </tbody>
      </Table>
    );
    const pollVoteForm = (
      <Table>
        <thead>
          <tr>
            <th>{poll.data.question}</th>
          </tr>
        </thead>
        <tbody>
          {poll.data.options.map((o, i) => {
            return (
              <tr key={i}>
                <td>
                  <Form.Check
                    onChange={this.handleChangeInputValue}
                    id={"formVoteOptionRadio" + i}
                    type="radio"
                    label={o.answerText}
                    name="selectedPollOption"
                    value={o.pollOptionId}
                    checked={this.state.selectedPollOption === o.pollOptionId}
                  />
                </td>
              </tr>
            );
          })}

          <tr>
            <td>
              <Button
                onClick={() => {
                  this.props.voteInPoll({
                    pollId: poll.data.pollId,
                    pollOptionId: this.state.selectedPollOption,
                    userId: auth.user.userId
                  });
                  this.setState({ selectedPollOption: "" });
                }}
                variant="danger"
                block
              >
                Glasaj
              </Button>
            </td>
            <td></td>
          </tr>
        </tbody>
      </Table>
    );
    return poll.isCurrentUserVoted ? pollResults : pollVoteForm;
  }
}

const mapStateToProps = state => {
  return {
    poll: state.poll,
    auth: state.auth
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators(
    {
      fetchLatestPoll,
      voteInPoll,
      checkIsUserVotedOnPoll
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(PollForm);
