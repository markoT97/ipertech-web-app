import React, { Component } from "react";
import { Form, Button, Modal, InputGroup } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setInsertUserContractModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { insertUserContract } from "../../redux/actions/userContractsActions/actionCreators";
import { Formik } from "formik";
import { insertUserContractValidationSchema as schema } from "../../shared/validation";
import { fetchPacketCombinations } from "./../../redux/actions/packetCombinationsActions/actionCreators";
import { userContractDurations } from "../../shared/constants";

export class InsertUserContractModal extends Component {
  componentDidMount() {
    this.props.fetchPacketCombinations();
  }
  handleOnSubmitUserContractForm = (form) => {
    this.props.insertUserContract({
      packetCombination: {
        packetCombinationId: form.packetCombination,
      },
      contractDurationMonths: form.duration,
    });
    this.props.setInsertUserContractModalVisibility(false);
  };
  render() {
    const { packetCombinations } = this.props;
    return (
      <Modal
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.modalsVisibility.insertUserContractModalVisibility}
        onHide={() => this.props.setInsertUserContractModalVisibility(false)}
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            Dodavanje novog korisničkog ugovora
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Formik
            validationSchema={schema}
            onSubmit={this.handleOnSubmitUserContractForm}
            initialValues={{
              packetCombination: "",
              duration: "",
            }}
          >
            {({
              handleSubmit,
              handleChange,
              handleBlur,
              values,
              touched,
              isValid,
              errors,
              dirty,
            }) => (
              <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formInternetPacketState">
                  <Form.Label>Kombinacija paketa</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text id="inputGroupPrependInternet">
                        <Icon.BoxArrowRight />
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      as="select"
                      name="packetCombination"
                      value={values.packetCombination}
                    >
                      <option value="" label="Izaberite kombinaciju paketa" />
                      {packetCombinations.map((packet, i) => {
                        return (
                          <option
                            key={i}
                            value={packet.packetCombinationId}
                            label={packet.name}
                          />
                        );
                      })}
                    </Form.Control>

                    {errors.packetCombination && (
                      <Form.Control.Feedback type="invalid">
                        {errors.packetCombination}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>
                <Form.Group controlId="formUserContractName">
                  <Form.Label>Trajanje ugovora</Form.Label>
                  <Form.Control
                    onChange={handleChange}
                    as="select"
                    name="duration"
                    value={values.duration}
                    isValid={touched.duration && !errors.duration}
                    isInvalid={!!errors.duration}
                  >
                    <option
                      value=""
                      label="Izaberite dužinu trajanja ugovora"
                    />
                    {userContractDurations.map((duration, i) => {
                      return (
                        <option key={i} value={duration} label={duration} />
                      );
                    })}
                  </Form.Control>

                  {errors.duration && (
                    <Form.Control.Feedback type="invalid">
                      {errors.duration}
                    </Form.Control.Feedback>
                  )}
                </Form.Group>

                <Button
                  onClick={() =>
                    this.props.setInsertUserContractModalVisibility(false)
                  }
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
    packetCombinations: state.packetCombinations,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    {
      setInsertUserContractModalVisibility,
      insertUserContract,
      fetchPacketCombinations,
    },
    dispatch
  );
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(InsertUserContractModal);
