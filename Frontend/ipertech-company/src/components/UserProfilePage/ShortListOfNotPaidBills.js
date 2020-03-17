import React, { Component } from "react";
import { Button, Table } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { format } from "date-fns";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { fetchUserById } from "../../redux/actions/userActions/actionCreators";
import { setTableOfBillsVisibility } from "../../redux/actions/tableOfBillsActions/actionCreators";
import { numberOfBillsPerPage } from "../../shared/constants";
import {
  fetchBills,
  fetchCountOfBills
} from "./../../redux/actions/billsActions/actionCreators";

export class ShortListOfNotPaidBills extends Component {
  render() {
    const { user, tableOfBills } = this.props;
    return (
      <Table striped responsive className="text-center">
        <thead>
          <tr>
            <th className="bg-danger text-white">
              <Icon.DocumentText size={35} />
              Računi
            </th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Neplaćeni računi</td>
          </tr>
          {user.bills.map((b, i) => {
            return (
              <tr key={i}>
                <td>
                  {format(new Date(b.startDate), "dd.MM.yyyy") +
                    " - " +
                    format(new Date(b.endDate), "dd.MM.yyyy")}
                </td>
              </tr>
            );
          })}

          <tr>
            <td className="bg-white">
              <Button
                onClick={() => {
                  this.props.setTableOfBillsVisibility(
                    !tableOfBills.visibility
                  );

                  this.props.fetchBills(
                    user.userContract.userContractId,
                    0,
                    numberOfBillsPerPage
                  );

                  this.props.fetchCountOfBills(
                    user.userContract.userContractId
                  );
                }}
                variant="outline-danger"
                block
              >
                {tableOfBills.visibility
                  ? "Sakrij račune"
                  : "Pregledaj sve račune"}
              </Button>
            </td>
          </tr>
        </tbody>
      </Table>
    );
  }
}

const mapStateToProps = state => {
  return {
    auth: state.auth,
    user: state.user,
    tableOfBills: state.tableOfBills
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators(
    { fetchUserById, fetchBills, fetchCountOfBills, setTableOfBillsVisibility },
    dispatch
  );
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ShortListOfNotPaidBills);
