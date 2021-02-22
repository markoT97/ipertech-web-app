import React from "react";
import { Alert } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { removeNotification } from "../redux/actions/notificationsActions/actionCreators";
import FadeIn from "react-fade-in";
import { notificationTypes } from "../shared/constants";
import { useDispatch, useSelector } from "react-redux";

const Notification = () => {
  const notifications = useSelector((state) => state.notifications);

  const dispatch = useDispatch();

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
              onClose={() => dispatch(removeNotification(n))}
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
};

export default Notification;
