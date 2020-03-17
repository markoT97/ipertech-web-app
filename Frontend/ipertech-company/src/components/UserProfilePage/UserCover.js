import React, { Component } from "react";
import { Jumbotron, Image, Spinner, Button } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { fetchUserById } from "./../../redux/actions/userActions/actionCreators";

export class UserCover extends Component {
  componentDidMount() {
    this.props.fetchUserById(this.props.auth.user.userId);
  }
  render() {
    const { user } = this.props;

    return (
      <Jumbotron
        fluid
        style={{
          backgroundImage: `url(${BACKEND_URL}account/cover-photo.jpg)`
        }}
      >
        <p className="text-center">
          {user.imageLocation ? (
            <Image
              className="border border-light"
              style={{ height: "10em" }}
              src={BACKEND_URL + user.imageLocation}
              roundedCircle
            />
          ) : (
            <Spinner
              variant="light"
              as="span"
              animation="border"
              style={{ width: "10em", height: "10em" }}
            />
          )}
        </p>
        <p className="text-center">
          <Button variant="light" size="sm">
            <Icon.Camera size={25} />
            &nbsp; Promeni sliku
          </Button>
        </p>
      </Jumbotron>
    );
  }
}

const mapStateToProps = state => {
  return {
    auth: state.auth,
    user: state.user
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchUserById }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(UserCover);
