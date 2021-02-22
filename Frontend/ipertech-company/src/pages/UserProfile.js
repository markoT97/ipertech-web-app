import React, { Component } from "react";
import { Row, Col } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import UserCover from "../components/UserProfile/UserCover";
import UserData from "../components/UserProfile/UserData";
import ShortListOfNotPaidBills from "../components/UserProfile/ShortListOfNotPaidBills";
import ChangePasswordForm from "../components/UserProfile/ChangePasswordForm";
import TableOfBills from "../components/UserProfile/TableOfBills";
import Messages from "../components/UserProfile/Messages";
import PollForm from "../components/UserProfile/PollForm";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import InsertMessageForm from "../components/UserProfile/InsertMessageForm";
import { Element } from "react-scroll";
import BillModal from "../components/UserProfile/BillModal";

export class UserProfile extends Component {
  render() {
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Moj nalog</h5>

        <UserCover />

        <BillModal />

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

        <Element className="table-of-bills">
          <TableOfBills />
        </Element>

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

const mapStateToProps = (state) => {
  return {};
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({}, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(UserProfile);
