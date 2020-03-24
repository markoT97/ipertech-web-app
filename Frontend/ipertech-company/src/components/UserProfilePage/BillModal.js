import React, { Component } from "react";
import { Modal } from "react-bootstrap";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setBillModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import Bill from "./Bill";

export class BillModal extends Component {
  render() {
    return (
      <Modal
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.modalsVisibility.billModalVisibility}
        onHide={() => this.props.setBillModalVisibility(false)}
      >
        <Modal.Body>
          <Bill />
        </Modal.Body>
      </Modal>
    );
  }
}

const mapStateToProps = state => {
  return {
    modalsVisibility: state.modalsVisibility
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ setBillModalVisibility }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(BillModal);
