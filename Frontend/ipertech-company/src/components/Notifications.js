import React, { Component } from "react";
import { Alert } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  addNotification,
  removeNotification
} from "../redux/actions/notificationsActions/actionCreators";
import FadeIn from "react-fade-in";
import { notificationTypes } from "../shared/constants";

export class Notification extends Component {
  render() {
    const { notifications } = this.props;
    return (
      <div style={{ position: "fixed", width: "20em", zIndex: 100, right: 0 }}>
        {notifications.map((n, i) => {
          return (
            <FadeIn key={i}>
              <Alert
                style={{ position: "relative" }}
                show={n.show}
                variant={
                  n.type === notificationTypes.INFO
                    ? "warning"
                    : n.type === notificationTypes.SUCCESS
                    ? "success"
                    : n.type === notificationTypes.ERROR
                    ? "danger"
                    : "secondary"
                }
                onClose={() => this.props.removeNotification(n)}
                dismissible
              >
                {n.type === "info" && (
                  <Icon.Info size={25} className="align-middle" />
                )}
                {n.type === "success" && (
                  <Icon.CheckCircle size={25} className="align-middle" />
                )}
                {n.type === "error" && (
                  <Icon.AlertCircle size={25} className="align-middle" />
                )}
                &nbsp;
                <span className="align-middle">{n.message}</span>
              </Alert>
            </FadeIn>
          );
        })}
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    notifications: state.notifications
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ addNotification, removeNotification }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Notification);
