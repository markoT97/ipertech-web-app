import React, { Component } from "react";
import { Table, Form, Button } from "react-bootstrap";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { fetchLatestPoll } from "./../../redux/actions/pollActions/actionCreators";

export class PollForm extends Component {
  componentDidMount() {
    this.props.fetchLatestPoll();
  }
  render() {
    const { poll } = this.props;
    return (
      <Table>
        <thead>
          <tr>
            <th>{poll.question}</th>
          </tr>
        </thead>
        <tbody>
          {poll.options.map((o, i) => {
            return (
              <tr key={i}>
                <td>
                  <Form.Check
                    type="radio"
                    label={o.answerText}
                    name="formHorizontalRadios"
                  />
                </td>
              </tr>
            );
          })}

          <tr>
            <td>
              <Button variant="danger" block>
                Glasaj
              </Button>
            </td>
            <td></td>
          </tr>
        </tbody>
      </Table>
    );
  }
}

const mapStateToProps = state => {
  return {
    poll: state.poll
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchLatestPoll }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(PollForm);
