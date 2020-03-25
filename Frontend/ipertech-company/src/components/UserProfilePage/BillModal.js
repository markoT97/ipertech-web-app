import React, { Component } from "react";
import { Modal, Button } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setBillModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { format } from "date-fns";
import Bill from "./Bill";
import { saveSvgAsPng } from "save-svg-as-png";

export class BillModal extends Component {
  handleOnBillSave = () => {
    const { selectedBill } = this.props.bills;
    saveSvgAsPng(
      document.getElementById("bill"),
      `racun-${format(new Date(selectedBill.startDate), "dd-MM-yyyy")}-${format(
        new Date(selectedBill.endDate),
        "dd-MM-yyyy"
      )}.png`,
      {
        scale: 1.5
      }
    );
  };
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
        <Modal.Footer>
          <Button
            onClick={() => this.handleOnBillSave()}
            variant="outline-danger"
            size="sm"
          >
            <Icon.Archive size={30} /> &nbsp; Sačuvaj
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }
}

const mapStateToProps = state => {
  return {
    modalsVisibility: state.modalsVisibility,
    bills: state.bills
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ setBillModalVisibility }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(BillModal);
