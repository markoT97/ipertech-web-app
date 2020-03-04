import React, { Component } from "react";
import { CardDeck } from "react-bootstrap";
import PeaceOfNews from "./PeaceOfNews";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchNotifications from "../../redux/actions/notificationsActions/actionCreators";

export class Notification extends Component {
  componentDidMount() {
    this.props.fetchNotifications("novosti");
  }
  render() {
    const news = this.props.notifications;
    return (
      <React.Fragment>
        <h5 className="text-danger text-uppercase">Obaveštenja</h5>
        <CardDeck>
          {news.map((n, i) => {
            return <PeaceOfNews key={i} piece={n} />;
          })}
        </CardDeck>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    notifications: state.notifications
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchNotifications }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Notification);
