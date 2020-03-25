import React, { Component } from "react";
import { Toast, Image, Button, Pagination, Row, Col } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import calculatePaginationOffset from "./../../utils/calculatePaginationOffset";
import { formatDistanceToNow } from "date-fns";
import { sr } from "date-fns/locale";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setMessagesCurrentPage } from "../../redux/actions/messagesActions/actionCreators";
import { numberOfMessagesPerPage } from "../../shared/constants";
import {
  fetchMessages,
  fetchCountOfMessages
} from "./../../redux/actions/messagesActions/actionCreators";
import { setInsertMessageModalVisibility } from "./../../redux/actions/modalsActions/actionCreators";

export class Messages extends Component {
  componentDidMount() {
    this.props.fetchMessages(0, numberOfMessagesPerPage);
    this.props.fetchCountOfMessages();
  }
  render() {
    const { userMessages, modalsVisibility } = this.props;

    let numberOfPages =
      userMessages.totalCount / numberOfMessagesPerPage +
      (userMessages.totalCount % numberOfMessagesPerPage > 0 ? 1 : 0);

    let paginationItems = [];
    for (let number = 1; number <= numberOfPages; number++) {
      paginationItems.push(
        <Pagination.Item
          onClick={() => {
            this.props.fetchMessages(
              calculatePaginationOffset(number, numberOfMessagesPerPage),
              numberOfMessagesPerPage
            );

            this.props.setMessagesCurrentPage(number);
          }}
          key={number}
          active={number === userMessages.currentPage}
        >
          {number}
        </Pagination.Item>
      );
    }
    return (
      <React.Fragment>
        {userMessages.data.map((um, i) => {
          return (
            <Toast key={i}>
              <Toast.Header>
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

        {userMessages.totalCount > numberOfMessagesPerPage && (
          <Row>
            <Col>
              <Pagination className="justify-content-center">
                <Pagination.First />
                <Pagination.Prev />
                {paginationItems}
                <Pagination.Ellipsis />
                <Pagination.Next />
                <Pagination.Last />
              </Pagination>
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
      setMessagesCurrentPage,
      setInsertMessageModalVisibility
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(Messages);
