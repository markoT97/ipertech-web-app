import React, { Component } from "react";
import { logoutUser } from "../redux/actions/authActions/actionCreators";
import { setLoginModalVisibility } from "../redux/actions/modalsActions/actionCreators";
import { addNotification } from "../redux/actions/notificationsActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Route, Redirect } from "react-router-dom";

class AuthRoute extends Component {
  render() {
    const { isAuthenticated, component: ForwardedComponent } = this.props;

    return (
      <Route
        render={(props) =>
          isAuthenticated ? (
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
});

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    { logoutUser, setLoginModalVisibility, addNotification },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(AuthRoute);
