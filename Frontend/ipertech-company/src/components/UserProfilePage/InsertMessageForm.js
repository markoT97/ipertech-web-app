import React, { Component } from "react";
import { Form, Button, Modal } from "react-bootstrap";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setInsertMessageModalVisibility } from "./../../redux/actions/modalsActions/actionCreators";
import {
  insertMessage,
  fetchMessages
} from "./../../redux/actions/messagesActions/actionCreators";

export class InsertMessageForm extends Component {
  state = {
    title: "",
    content: ""
  };
  handleChangeInputValue = e => {
    const { target } = e;
    this.setState({ [target.name]: target.value });
  };
  handleOnSubmitMessageForm = e => {
    e.preventDefault();
    this.props.insertMessage(
      {
        userId: this.props.user.userId
      },
      {
        title: this.state.title,
        content: this.state.content
      }
    );
    this.props.setInsertMessageModalVisibility(false);

    this.setState({ title: "", content: "" });
  };
  render() {
    return (
      <Modal
        onSubmit={this.handleOnSubmitMessageForm}
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.modalsVisibility.insertMessageModalVisibility}
        onHide={() => this.props.setInsertMessageModalVisibility(false)}
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            Slanje poruke
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formMessageTitle">
              <Form.Label>Naslov</Form.Label>
              <Form.Control
                onChange={this.handleChangeInputValue}
                type="text"
                name="title"
                placeholder="Unesite naslov poruke"
                value={this.state.title}
              />
            </Form.Group>

            <Form.Group controlId="formMessageContent">
              <Form.Label>Tekst</Form.Label>
              <Form.Control
                onChange={this.handleChangeInputValue}
                as="textarea"
                name="content"
                placeholder="Unesite sadržaj poruke"
                value={this.state.content}
                rows="3"
              />
            </Form.Group>

            <Button className="float-right ml-2" variant="light">
              Odustani
            </Button>
            <Button
              className="float-right ml-2"
              variant="primary"
              type="submit"
            >
              Pošalji
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    );
  }
}

const mapStateToProps = state => {
  return {
    modalsVisibility: state.modalsVisibility,
    userMessages: state.userMessages,
    user: state.user
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators(
    {
      setInsertMessageModalVisibility,
      insertMessage,
      fetchMessages
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(InsertMessageForm);
