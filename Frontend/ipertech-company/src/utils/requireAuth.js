import React, { Component } from "react";
import setVisibility from "../redux/actions/loginModalActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

export default function(ComposedComponent) {
  class Authenticate extends Component {
    componentDidMount() {
      const { auth } = this.props;
      if (!auth.isAuthenticated) {
        console.log("User is not authenticated");
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
    return bindActionCreators({ setVisibility }, dispatch);
  };

  return connect(mapStateToProps, mapDispatchToProps)(Authenticate);
}
