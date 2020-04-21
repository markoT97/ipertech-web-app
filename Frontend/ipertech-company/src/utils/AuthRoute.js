import React, { Component } from "react";
import { logoutUser } from "../redux/actions/authActions/actionCreators";
import { setLoginModalVisibility } from "../redux/actions/modalsActions/actionCreators";
import { addNotification } from "../redux/actions/notificationsActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Route, Redirect } from "react-router-dom";

class AuthRoute extends Component {
  render() {
    const {
      isAuthenticated,
      component: ForwardedComponent,
      currentRole,
      allowedRole,
    } = this.props;

    return (
      <Route
        render={(props) =>
          isAuthenticated && currentRole === allowedRole ? (
            <ForwardedComponent />
          ) : (
            <Redirect
              to={{
                pathname: "/",
                state: { from: props.location },
              }}
            />
          )
        }
      />
    );
  }
}

const mapStateToProps = (state) => ({
  isAuthenticated: state.auth.isAuthenticated,
  currentRole: state.auth.user.role,
});

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    { logoutUser, setLoginModalVisibility, addNotification },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(AuthRoute);
