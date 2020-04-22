import React, { Component } from "react";
import { Form, Button, Modal, InputGroup } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setInsertPacketCombinationModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { insertPacketCombination } from "../../redux/actions/packetCombinationsActions/actionCreators";
import { Formik } from "formik";
import { insertPacketCombinationValidationSchema as schema } from "../../shared/validation";
import fetchInternetPackets from "./../../redux/actions/internetPacketsActions/actionCreators";
import fetchTvPackets from "./../../redux/actions/tvPacketsActions/actionCreators";
import fetchPhonePackets from "./../../redux/actions/phonePacketsActions/actionCreators";

export class InsertPacketCombinationModal extends Component {
  componentDidMount() {
    this.props.fetchInternetPackets();
    this.props.fetchTvPackets();
    this.props.fetchPhonePackets();
  }
  handleOnSubmitPacketCombinationForm = (form) => {
    const internetPacket = JSON.parse(form.internetPacket);
    this.props.insertPacketCombination({
      name: form.name,
      internetPacket: {
        internetPacketId: internetPacket.internetPacketId,
        internetRouter: {
          internetRouterId: internetPacket.internetRouterId,
        },
      },
      tvPacket: form.tvPacketId ? { tvPacketId: form.tvPacket } : undefined,
      phonePacket: form.phonePacketId
        ? { phonePacketId: form.phonePacket }
        : undefined,
    });
    this.props.setInsertPacketCombinationModalVisibility(false);
  };
  render() {
    const { internetPackets, tvPackets, phonePackets } = this.props;
    return (
      <Modal
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={
          this.props.modalsVisibility.insertPacketCombinationModalVisibility
        }
        onHide={() =>
          this.props.setInsertPacketCombinationModalVisibility(false)
        }
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            Dodavanje nove kombinacije paketa
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Formik
            validationSchema={schema}
            onSubmit={this.handleOnSubmitPacketCombinationForm}
            initialValues={{
              name: "",
              internetPacket: "",
              tvPacket: undefined,
              phonePacket: undefined,
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
                <Form.Group controlId="formPacketCombinationName">
                  <Form.Label>Naziv</Form.Label>
                  <Form.Control
                    onChange={handleChange}
                    type="text"
                    name="name"
                    placeholder="Unesite naziv paketa"
                    value={values.name}
                    isValid={touched.name && !errors.name}
                    isInvalid={!!errors.name}
                  />

                  {errors.name && (
                    <Form.Control.Feedback type="invalid">
                      {errors.name}
                    </Form.Control.Feedback>
                  )}
                </Form.Group>

                <Form.Group controlId="formInternetPacketState">
                  <Form.Label>Internet</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text id="inputGroupPrependInternet">
                        @
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      as="select"
                      name="internetPacket"
                      value={values.internetPacket}
                    >
                      <option value="" label="Izaberite internet paket" />
                      {internetPackets.map((internet, i) => {
                        return (
                          <option
                            key={i}
                            value={JSON.stringify({
                              internetPacketId: internet.internetPacketId,
                              internetRouterId:
                                internet.internetRouter.internetRouterId,
                            })}
                            label={internet.name}
                          />
                        );
                      })}
                    </Form.Control>

                    {errors.internetPacket && (
                      <Form.Control.Feedback type="invalid">
                        {errors.internetPacket}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formTvPacketState">
                  <Form.Label>Televizija</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text id="inputGroupPrependTv">
                        <Icon.Tv />
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      as="select"
                      name="tvPacket"
                      value={values.tvPacket}
                    >
                      <option value={undefined} label="Izaberite tv paket" />
                      {tvPackets.map((tv, i) => {
                        return (
                          <option
                            key={i}
                            value={tv.tvPacketId}
                            label={tv.name}
                          />
                        );
                      })}
                    </Form.Control>

                    {errors.tvPacket && (
                      <Form.Control.Feedback type="invalid">
                        {errors.tvPacket}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formPhonePacketState">
                  <Form.Label>Telefonija</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text id="inputGroupPrependPhone">
                        <Icon.Phone />
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      as="select"
                      name="phonePacket"
                      value={values.phonePacket}
                    >
                      <option
                        value={undefined}
                        label="Izaberite telefonski paket"
                      />
                      {phonePackets.map((phone, i) => {
                        return (
                          <option
                            key={i}
                            value={phone.phonePacketId}
                            label={phone.name}
                          />
                        );
                      })}
                    </Form.Control>

                    {errors.internetPacket && (
                      <Form.Control.Feedback type="invalid">
                        {errors.phonePacket}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Button
                  onClick={() =>
                    this.props.setInsertPacketCombinationModalVisibility(false)
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
    internetPackets: state.internetPackets,
    tvPackets: state.tvPackets,
    phonePackets: state.phonePackets,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    {
      setInsertPacketCombinationModalVisibility,
      insertPacketCombination,
      fetchInternetPackets,
      fetchTvPackets,
      fetchPhonePackets,
    },
    dispatch
  );
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(InsertPacketCombinationModal);
