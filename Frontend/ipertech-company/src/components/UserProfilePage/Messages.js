import React, { Component } from "react";
import { Toast } from "react-bootstrap";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

export class Messages extends Component {
  render() {
    return (
      <React.Fragment>
        <Toast>
          <Toast.Header>
            <img
              src="holder.js/20x20?text=%20"
              className="rounded mr-2"
              alt=""
            />
            <strong className="mr-auto">Ime korisnika</strong>
            <small>11 mins ago</small>
          </Toast.Header>
          <Toast.Body>Tekst poruke</Toast.Body>
        </Toast>
        <Toast>
          <Toast.Header>
            <img
              src="holder.js/20x20?text=%20"
              className="rounded mr-2"
              alt=""
            />
            <strong className="mr-auto">Ime korisnika</strong>
            <small>11 mins ago</small>
          </Toast.Header>
          <Toast.Body>Tekst poruke</Toast.Body>
        </Toast>
        <Toast>
          <Toast.Header>
            <img
              src="holder.js/20x20?text=%20"
              className="rounded mr-2"
              alt=""
            />
            <strong className="mr-auto">Ime korisnika</strong>
            <small>11 mins ago</small>
          </Toast.Header>
          <Toast.Body>Tekst poruke</Toast.Body>
        </Toast>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    poll: state.poll
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({}, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Messages);
