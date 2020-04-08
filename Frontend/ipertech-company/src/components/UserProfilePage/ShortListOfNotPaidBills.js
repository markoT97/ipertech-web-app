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

import { Link } from "react-scroll";

export class ShortListOfNotPaidBills extends Component {
  render() {
    const { user, tableOfBills } = this.props;
    return (
      <Table striped responsive className="text-center short-list-of-bills">
        <thead>
          <tr>
            <th className="bg-danger text-white">
              <Icon.DocumentText size={35} />
              Računi
            </th>
          </tr>
        </thead>
        <tbody>
          {user.bills && (
            <tr>
              <td>Neplaćeni računi</td>
            </tr>
          )}

          {user.bills.map((b, i) => {
            return (
              <tr key={i}>
                <td>
                  {b ? (
                    format(new Date(b.startDate), "dd.MM.yyyy") +
                    " - " +
                    format(new Date(b.endDate), "dd.MM.yyyy")
                  ) : (
                    <React.Fragment>
                      <p>Svi računi su plaćeni!</p>
                      <Icon.CheckCircle size={50} className="text-success" />
                    </React.Fragment>
                  )}
                </td>
              </tr>
            );
          })}

          <tr>
            <td className="bg-white">
              <Link
                activeClass="active"
                to={
                  tableOfBills.visibility
                    ? "short-list-of-bills"
                    : "table-of-bills"
                }
                spy={true}
                smooth={true}
                duration={500}
              >
                {user.bills.length > 0 && user.bills[0] && (
                  <Button
                    onClick={() => {
                      this.props.setTableOfBillsVisibility(
                        !tableOfBills.visibility
                      );
                      this.props.fetchBills(
                        user.userContract.userContractId,
                        (Math.floor(
                          (tableOfBills.currentPage * numberOfBillsPerPage) /
                            numberOfBillsPerPage
                        ) -
                          1) *
                          numberOfBillsPerPage,
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
                )}
              </Link>
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
