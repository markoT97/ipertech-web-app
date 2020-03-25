import React, { Component } from "react";
import { Row, Col } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import "./../App.scss";
import UserCover from "./UserCover";
import UserData from "./UserData";
import ShortListOfNotPaidBills from "./ShortListOfNotPaidBills";
import ChangePasswordForm from "./ChangePasswordForm";
import TableOfBills from "./TableOfBills";
import Messages from "./Messages";
import PollForm from "./PollForm";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

import InsertMessageForm from "./InsertMessageForm";

export class UserProfile extends Component {
  render() {
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Moj nalog</h5>

        <UserCover />

        <Row>
          <Col lg={6}>
            <UserData />
          </Col>
          <Col lg={3}>
            <ShortListOfNotPaidBills />
          </Col>
          <Col lg={3}>
            <ChangePasswordForm />
          </Col>
        </Row>

        <TableOfBills />

        <Row>
          <Col lg={5} className="mt-2 mb-2">
            <h5 className="text-warning text-uppercase text-center">
              <Icon.Chat size={40} />
              &nbsp; Poruke
            </h5>
            <Messages />

            <InsertMessageForm />
          </Col>
          <Col className="text-center mt-2">
            <h5 className="text-danger text-uppercase">
              <Icon.QuestionFill size={40} />
              &nbsp; Anketa
            </h5>
            <PollForm />
          </Col>
        </Row>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {};
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({}, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(UserProfile);
