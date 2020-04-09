import React, { Component } from "react";
import { Form, Button, Modal } from "react-bootstrap";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setInsertMessageModalVisibility } from "./../../redux/actions/modalsActions/actionCreators";
import {
  insertMessage,
  fetchMessages
} from "./../../redux/actions/messagesActions/actionCreators";
import { Formik } from "formik";
import { insertMessageValidationSchema as schema } from "./../../shared/validation";

export class InsertMessageForm extends Component {
  handleOnSubmitMessageForm = form => {
    this.props.insertMessage(
      {
        userId: this.props.user.userId,
        imageLocation: this.props.user.imageLocation
      },
      {
        title: form.title,
        content: form.content
      }
    );
    this.props.setInsertMessageModalVisibility(false);
  };
  render() {
    return (
      <Modal
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
          <Formik
            validationSchema={schema}
            onSubmit={this.handleOnSubmitMessageForm}
            initialValues={{ title: "", content: "" }}
          >
            {({
              handleSubmit,
              handleChange,
              handleBlur,
              values,
              touched,
              isValid,
              errors,
              dirty
            }) => (
              <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formMessageTitle">
                  <Form.Label>Naslov</Form.Label>
                  <Form.Control
                    onChange={handleChange}
                    type="text"
                    name="title"
                    placeholder="Unesite naslov poruke"
                    value={values.title}
                    isValid={touched.title && !errors.title}
                    isInvalid={!!errors.title}
                  />

                  {errors.title && (
                    <Form.Control.Feedback type="invalid">
                      {errors.title}
                    </Form.Control.Feedback>
                  )}
                </Form.Group>

                <Form.Group controlId="formMessageContent">
                  <Form.Label>Tekst</Form.Label>
                  <Form.Control
                    onChange={handleChange}
                    as="textarea"
                    name="content"
                    placeholder="Unesite sadržaj poruke"
                    value={values.content}
                    rows="3"
                    isValid={touched.content && !errors.content}
                    isInvalid={!!errors.content}
                  />

                  {errors.content && (
                    <Form.Control.Feedback type="invalid">
                      {errors.content}
                    </Form.Control.Feedback>
                  )}
                </Form.Group>

                <Button
                  onClick={() =>
                    this.props.setInsertMessageModalVisibility(false)
                  }
                  className="float-right ml-2"
                  variant="light"
                >
                  Odustani
                </Button>
                <Button
                  className="float-right ml-2"
                  type="submit"
                  variant="primary"
                  disabled={!(isValid && dirty)}
                >
                  Pošalji
                </Button>
              </Form>
            )}
          </Formik>
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
