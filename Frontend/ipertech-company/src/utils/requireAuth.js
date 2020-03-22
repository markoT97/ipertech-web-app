import React, { Component } from "react";
import { fromUnixTime } from "date-fns";
import { logoutUser } from "./../redux/actions/authActions/actionCreators";
import setVisibility from "../redux/actions/loginModalActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

export default function(ComposedComponent) {
  class Authenticate extends Component {
    componentDidMount() {
      const { auth } = this.props;
      if (!auth.isAuthenticated || Date.now() > fromUnixTime(auth.user.exp)) {
        console.log("User is not authenticated");
        this.props.logoutUser();
        this.props.history.push("/");
        this.props.setVisibility(true);
      }
    }

    UNSAFE_componentWillUpdate(nextProps) {
      if (!nextProps.auth.isAuthenticated) {
        this.props.history.push("/");
        this.props.setVisibility(true);
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
    return bindActionCreators({ logoutUser, setVisibility }, dispatch);
  };

  return connect(mapStateToProps, mapDispatchToProps)(Authenticate);
}
