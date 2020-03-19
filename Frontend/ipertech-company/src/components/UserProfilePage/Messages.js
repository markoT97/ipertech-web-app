import React, { Component } from "react";
import { Toast, Image, Pagination } from "react-bootstrap";
import { formatDistanceToNow } from "date-fns";
import { sr } from "date-fns/locale";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { numberOfMessagesPerPage } from "../../shared/constants";
import {
  fetchMessages,
  fetchCountOfMessages
} from "./../../redux/actions/messagesActions/actionCreators";

export class Messages extends Component {
  componentDidMount() {
    this.props.fetchMessages(0, numberOfMessagesPerPage);
  }
  render() {
    const { auth, userMessages } = this.props;

    let numberOfPages =
      userMessages.totalCount / numberOfMessagesPerPage +
      (userMessages.totalCount % numberOfMessagesPerPage > 0 ? 1 : 0);

    let paginationItems = [];
    for (let number = 1; number <= numberOfPages; number++) {
      paginationItems.push(
        <Pagination.Item
          onClick={() => {
            this.props.fetchBills(
              auth.user.userId,
              (Math.floor(
                (number * numberOfMessagesPerPage) / numberOfMessagesPerPage
              ) -
                1) *
                numberOfMessagesPerPage,
              numberOfMessagesPerPage
            );

            //this.props.setTableOfBillsCurrentPage(number);
          }}
          key={number}
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

        {userMessages.data.length > numberOfMessagesPerPage && (
          <Pagination className="justify-content-center">
            <Pagination.First />
            <Pagination.Prev />
            {paginationItems}
            <Pagination.Ellipsis />
            <Pagination.Next />
            <Pagination.Last />
          </Pagination>
        )}
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    auth: state,
    userMessages: state.userMessages
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchMessages, fetchCountOfMessages }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Messages);
