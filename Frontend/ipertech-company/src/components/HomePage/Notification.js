import React, { Component } from "react";
import { CardDeck, Card } from "react-bootstrap";
import FadeIn from "react-fade-in";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchNotifications from "../../redux/actions/notificationsActions/actionCreators";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";

export class Notification extends Component {
  componentDidMount() {
    this.props.fetchNotifications("novosti");
  }
  render() {
    const notifications = this.props.notifications;
    return (
      <React.Fragment>
        <h5 className="text-danger text-uppercase">Obaveštenja</h5>
        <CardDeck>
          {notifications.map((n, i) => {
            return (
              <Card key={i} bg="secondary" text="white">
                <Card.Img
                  src={BACKEND_URL + "/" + n.imageLocation}
                  style={{ opacity: 0.6 }}
                />
                <Card.ImgOverlay>
                  <Card.Title>{n.title}</Card.Title>
                  <FadeIn>
                    <Card.Text>{n.content}</Card.Text>
                  </FadeIn>
                </Card.ImgOverlay>
              </Card>
            );
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
