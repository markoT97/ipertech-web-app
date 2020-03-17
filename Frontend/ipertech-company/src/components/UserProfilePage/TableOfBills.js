import React, { Component } from "react";
import { Button, Table, Row, Col, Pagination } from "react-bootstrap";
import { format } from "date-fns";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { fetchUserById } from "../../redux/actions/userActions/actionCreators";
import {
  setTableOfBillsVisibility,
  setTableOfBillsCurrentPage
} from "../../redux/actions/tableOfBillsActions/actionCreators";
import { numberOfBillsPerPage } from "../../shared/constants";
import {
  fetchBills,
  fetchCountOfBills
} from "./../../redux/actions/billsActions/actionCreators";

export class TableOfBills extends Component {
  render() {
    const { user, bills, tableOfBills } = this.props;

    let numberOfPages =
      bills.totalCount / numberOfBillsPerPage +
      (bills.totalCount % numberOfBillsPerPage > 0 ? 1 : 0);

    let paginationItems = [];
    for (let number = 1; number <= numberOfPages; number++) {
      paginationItems.push(
        <Pagination.Item
          onClick={() => {
            this.props.fetchBills(
              user.userContract.userContractId,
              (Math.floor(
                (number * numberOfBillsPerPage) / numberOfBillsPerPage
              ) -
                1) *
                numberOfBillsPerPage,
              numberOfBillsPerPage
            );

            this.props.setTableOfBillsCurrentPage(number);
          }}
          key={number}
          active={number === tableOfBills.currentPage}
        >
          {number}
        </Pagination.Item>
      );
    }

    return (
      <React.Fragment>
        {tableOfBills.visibility ? (
          <Row>
            <Col>
              <Table className="text-center" borderless>
                <thead className="text-danger">
                  <tr>
                    <th>Obračunski period</th>
                    <th>Poziv na broj</th>
                    <th>Račun primaoca</th>
                  </tr>
                </thead>
                <tbody>
                  {bills.data.map((b, i) => {
                    return (
                      <React.Fragment key={i}>
                        <Button
                          as="tr"
                          variant={
                            b.isPaid ? "outline-success" : "outline-danger"
                          }
                          style={{ display: "table-row" }}
                        >
                          <td>
                            {format(new Date(b.startDate), "dd.MM.yyyy") +
                              " - " +
                              format(new Date(b.endDate), "dd.MM.yyyy")}
                          </td>
                          <td>{b.callNum}</td>
                          <td>{b.accOfRecipient}</td>
                        </Button>
                        <tr className="mb-2">
                          <td></td>
                        </tr>
                      </React.Fragment>
                    );
                  })}
                </tbody>
              </Table>

              <Pagination className="justify-content-center">
                <Pagination.First />
                <Pagination.Prev />
                {paginationItems}
                <Pagination.Ellipsis />
                <Pagination.Next />
                <Pagination.Last />
              </Pagination>
            </Col>
          </Row>
        ) : (
          ""
        )}
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user,
    bills: state.bills,
    tableOfBills: state.tableOfBills
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators(
    {
      fetchUserById,
      fetchBills,
      fetchCountOfBills,
      setTableOfBillsVisibility,
      setTableOfBillsCurrentPage
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(TableOfBills);
