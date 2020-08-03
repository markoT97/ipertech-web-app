import React, { Component } from "react";
import { Form, Button, Modal, InputGroup } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setInsertNewsModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { insertNews } from "../../redux/actions/newsActions/actionCreators";
import { Formik } from "formik";
import { insertNewsValidationSchema as schema } from "../../shared/validation-schemas";
import { fetchPacketCombinations } from "./../../redux/actions/packetCombinationsActions/actionCreators";
import { FilePicker } from "react-file-picker";

export class InsertNewsModal extends Component {
  handleOnSubmitNewsForm = (form) => {
    this.props.insertNews({
      title: form.title,
      content: form.content,
      image: form.image,
    });
    this.props.setInsertNewsModalVisibility(false);
  };
  render() {
    return (
      <Modal
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.modalsVisibility.insertNewsModalVisibility}
        onHide={() => this.props.setInsertNewsModalVisibility(false)}
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            Dodavanje novosti
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Formik
            validationSchema={schema}
            onSubmit={this.handleOnSubmitNewsForm}
            initialValues={{
              title: "",
              content: "",
              image: "",
            }}
          >
            {({
              handleSubmit,
              handleChange,
              handleBlur,
              setFieldValue,
              values,
              touched,
              isValid,
              errors,
              dirty,
            }) => (
              <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formNewsName">
                  <Form.Label>Naslov</Form.Label>
                  <Form.Control
                    onChange={handleChange}
                    type="text"
                    name="title"
                    placeholder="Unesite naslov novosti"
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

                <Form.Group controlId="formNewsContent">
                  <Form.Label>Sadržaj</Form.Label>
                  <Form.Control
                    onChange={handleChange}
                    type="text"
                    name="content"
                    placeholder="Unesite sadržaj novosti"
                    value={values.content}
                    isValid={touched.content && !errors.content}
                    isInvalid={!!errors.content}
                  />

                  {errors.content && (
                    <Form.Control.Feedback type="invalid">
                      {errors.content}
                    </Form.Control.Feedback>
                  )}
                </Form.Group>

                <Form.Group>
                  <Form.Label>Slika</Form.Label>
                  <FilePicker
                    extensions={["jpg", "jpeg", "png"]}
                    dims={{
                      minWidth: 100,
                      maxWidth: 500,
                      minHeight: 100,
                      maxHeight: 500,
                    }}
                    onChange={(fileObject) =>
                      setFieldValue("image", fileObject)
                    }
                    onError={(errMsg) => setFieldValue("image", errMsg)}
                  >
                    <InputGroup className="mb-3">
                      <InputGroup.Prepend>
                        <Button variant="outline-primary" size="sm">
                          <Icon.Image size={25} />
                        </Button>
                      </InputGroup.Prepend>
                      <Form.Control
                        placeholder={
                          values.image.name
                            ? values.image.name
                            : errors.image
                            ? values.image
                            : "Izaberite sliku"
                        }
                        aria-label="Izaberite sliku"
                        aria-describedby="basic-addon2"
                        readOnly
                        onChange={(fileObject) =>
                          setFieldValue("image", fileObject)
                        }
                        onError={(errMsg) => setFieldValue("image", errMsg)}
                        isValid={touched.image && !errors.image}
                        isInvalid={!!errors.image}
                      />
                      {errors.image && (
                        <Form.Control.Feedback type="invalid">
                          {errors.image}
                        </Form.Control.Feedback>
                      )}
                    </InputGroup>
                  </FilePicker>
                </Form.Group>

                <Button
                  onClick={() => this.props.setInsertNewsModalVisibility(false)}
                  className="float-right ml-2"
                  variant="light"
                >
                  Odustani
                </Button>
                <Button
                  className="float-right ml-2"
                  type="submit"
                  variant="success"
                  disabled={!(isValid && dirty)}
                >
                  Dodaj
                </Button>
              </Form>
            )}
          </Formik>
        </Modal.Body>
      </Modal>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    modalsVisibility: state.modalsVisibility,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    {
      setInsertNewsModalVisibility,
      insertNews,
      fetchPacketCombinations,
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(InsertNewsModal);
