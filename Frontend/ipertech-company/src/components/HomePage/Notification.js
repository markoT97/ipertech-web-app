import React, { Component } from "react";
import { CardDeck, Card } from "react-bootstrap";
import FadeIn from "react-fade-in";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchNotifications from "../../redux/actions/notificationsActions/actionCreators";

export class Notification extends Component {
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
                  src="https://images.unsplash.com/photo-1519389950473-47ba0277781c?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80"
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
  return bindActionCreators(fetchNotifications, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Notification);
