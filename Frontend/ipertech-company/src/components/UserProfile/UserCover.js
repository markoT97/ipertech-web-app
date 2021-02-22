import React, { Component } from "react";
import { Jumbotron, Image, Spinner, Button } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { BACKEND_URL } from "../../shared/constants";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  fetchUserByContractId,
  updateUserImage,
} from "./../../redux/actions/userActions/actionCreators";
import { FilePicker } from "react-file-picker";

export class UserCover extends Component {
  componentDidMount() {
    this.props.fetchUserByContractId(this.props.auth.user.userContractId);
  }
  render() {
    const { user } = this.props;

    return (
      <Jumbotron
        fluid
        style={{
          backgroundImage: `url(${BACKEND_URL}account/cover-photo.jpg)`,
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
        <span className="text-center">
          <FilePicker
            extensions={["jpg", "jpeg", "png"]}
            dims={{
              minWidth: 100,
              maxWidth: 500,
              minHeight: 100,
              maxHeight: 500,
            }}
            onChange={(fileObject) =>
              this.props.updateUserImage({
                userId: this.props.auth.user.userId,
                image: fileObject,
              })
            }
            onError={(errMsg) => console.error(errMsg)}
          >
            <Button variant="light" size="sm">
              <Icon.Camera size={25} />
              &nbsp; Promeni sliku
            </Button>
          </FilePicker>
        </span>
      </Jumbotron>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    auth: state.auth,
    user: state.user,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    { fetchUserByContractId, updateUserImage },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(UserCover);
