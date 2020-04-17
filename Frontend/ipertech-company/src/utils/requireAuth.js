import React, { Component } from "react";
import { fromUnixTime } from "date-fns";
import { logoutUser } from "./../redux/actions/authActions/actionCreators";
import { setLoginModalVisibility } from "../redux/actions/modalsActions/actionCreators";
import { addNotification } from "../redux/actions/notificationsActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { notificationTypes } from "../shared/constants";

export default function(ComposedComponent) {
  class Authenticate extends Component {
    componentDidMount() {
      const { auth } = this.props;
      if (!auth.isAuthenticated || Date.now() > fromUnixTime(auth.user.exp)) {
        this.props.addNotification({
          type: notificationTypes.ERROR,
          message: "Niste prijavljeni",
          duration: 5000
        });
        this.props.logoutUser();
      }
    }

    UNSAFE_componentWillUpdate(nextProps) {
      if (!nextProps.auth.isAuthenticated) {
        this.props.history.push("/");
        this.props.setLoginModalVisibility(true);
      }
    }

    render() {
      return <ComposedComponent {...this.props} />;
    }
  }

  const mapStateToProps = state => {
    return {
      auth: state.auth
    };
  };

  const mapDispatchToProps = dispatch => {
    return bindActionCreators(
      { logoutUser, setLoginModalVisibility, addNotification },
      dispatch
    );
  };

  return connect(mapStateToProps, mapDispatchToProps)(Authenticate);
}
