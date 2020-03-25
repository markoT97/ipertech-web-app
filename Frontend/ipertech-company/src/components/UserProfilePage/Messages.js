import React, { Component } from "react";
import { Toast, Image, Button, Row, Col } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { formatDistanceToNow } from "date-fns";
import { sr } from "date-fns/locale";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setMessagesCurrentPage } from "../../redux/actions/messagesActions/actionCreators";
import { numberOfMessagesPerPage } from "../../shared/constants";
import {
  fetchMessages,
  fetchCountOfMessages,
  deleteMessage
} from "./../../redux/actions/messagesActions/actionCreators";
import { setInsertMessageModalVisibility } from "./../../redux/actions/modalsActions/actionCreators";

export class Messages extends Component {
  componentDidMount() {
    this.props.fetchMessages(0, numberOfMessagesPerPage);
    this.props.fetchCountOfMessages();
  }

  render() {
    const { userMessages, modalsVisibility } = this.props;

    return (
      <React.Fragment>
        {userMessages.data.map((um, i) => {
          return (
            <Toast
              key={i}
              onClose={() => this.props.deleteMessage(um.message.messageId)}
            >
              <Toast.Header
                closeButton={um.user.userId === this.props.auth.user.userId}
              >
                <Image
                  style={{ height: "2em" }}
                  src={BACKEND_URL + "/" + um.user.imageLocation}
                  className="rounded mr-2"
                  alt=""
                />
                <strong className="mr-auto">{um.message.title}</strong>
                <small>
                  {formatDistanceToNow(new Date(um.message.createdAt), {
                    locale: sr,
                    addSuffix: true
                  })}
                </small>
              </Toast.Header>
              <Toast.Body>{um.message.content}</Toast.Body>
            </Toast>
          );
        })}

        {!modalsVisibility.insertMessageModalVisibility && (
          <Row>
            <Col>
              <Button
                size="sm"
                className="float-right mb-3"
                variant="outline-secondary"
                onClick={() => this.props.setInsertMessageModalVisibility(true)}
              >
                <Icon.Cursor size={26} />
                Pošalji
              </Button>
            </Col>
          </Row>
        )}
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    auth: state,
    userMessages: state.userMessages,
    modalsVisibility: state.modalsVisibility
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators(
    {
      fetchMessages,
      fetchCountOfMessages,
      deleteMessage,
      setMessagesCurrentPage,
      setInsertMessageModalVisibility
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(Messages);
