import React, { Component } from "react";
import { Button, Table, Row, Col, Pagination } from "react-bootstrap";
import calculatePaginationOffset from "./../../utils/calculatePaginationOffset";
import { format } from "date-fns";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { fetchUserByContractId } from "../../redux/actions/userActions/actionCreators";
import {
  setTableOfBillsVisibility,
  setTableOfBillsCurrentPage
} from "../../redux/actions/tableOfBillsActions/actionCreators";
import { setBillModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { numberOfBillsPerPage } from "../../shared/constants";
import {
  fetchBills,
  fetchSelectedBill
} from "./../../redux/actions/billsActions/actionCreators";

const numberOfPageItemsInRow = 3;

export class TableOfBills extends Component {
  handleOnBillClick = selectedBill => {
    this.props.fetchSelectedBill(selectedBill);
    this.props.setBillModalVisibility(true);
  };
  render() {
    const { user, bills, tableOfBills } = this.props;

    let numberOfPages = Math.trunc(
      bills.totalCount / numberOfBillsPerPage +
        (bills.totalCount % numberOfBillsPerPage > 0 ? 1 : 0)
    );

    let paginationItems = [];
    for (
      let number =
        tableOfBills.currentPage + 1 < numberOfPages
          ? tableOfBills.currentPage
          : tableOfBills.currentPage - 2;
      number <=
        tableOfBills.currentPage +
          numberOfPageItemsInRow -
          (numberOfPages % 2 !== 0 ? 1 : 0) -
          1 && number <= numberOfPages;
      number++
    ) {
      paginationItems.push(
        <Pagination.Item
          onClick={() => {
            this.props.fetchBills(
              user.userContract.userContractId,
              calculatePaginationOffset(number, numberOfBillsPerPage),
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
                          onClick={() => this.handleOnBillClick(b)}
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

              {bills.totalCount > numberOfBillsPerPage && (
                <Pagination className="justify-content-center">
                  {tableOfBills.currentPage > 1 && (
                    <Pagination.First
                      onClick={() => {
                        this.props.fetchBills(
                          user.userContract.userContractId,
                          calculatePaginationOffset(1, numberOfBillsPerPage),
                          numberOfBillsPerPage
                        );
                        this.props.setTableOfBillsCurrentPage(1);
                      }}
                    />
                  )}
                  {tableOfBills.currentPage > 1 && (
                    <Pagination.Prev
                      onClick={() => {
                        if (tableOfBills.currentPage !== 1) {
                          this.props.fetchBills(
                            user.userContract.userContractId,
                            calculatePaginationOffset(
                              tableOfBills.currentPage - 1,
                              numberOfBillsPerPage
                            ),
                            numberOfBillsPerPage
                          );
                          this.props.setTableOfBillsCurrentPage(
                            tableOfBills.currentPage - 1
                          );
                        }
                      }}
                    />
                  )}
                  {tableOfBills.currentPage - 1 >= 1 && (
                    <Pagination.Ellipsis
                      onClick={() => {
                        this.props.fetchBills(
                          user.userContract.userContractId,
                          calculatePaginationOffset(
                            paginationItems[0].props.children - 1,
                            numberOfBillsPerPage
                          ),
                          numberOfBillsPerPage
                        );
                        this.props.setTableOfBillsCurrentPage(
                          paginationItems[0].props.children - 1
                        );
                      }}
                    />
                  )}
                  {paginationItems}
                  {tableOfBills.currentPage + 1 < numberOfPages && (
                    <Pagination.Ellipsis
                      onClick={() => {
                        this.props.fetchBills(
                          user.userContract.userContractId,
                          calculatePaginationOffset(
                            tableOfBills.currentPage + numberOfPageItemsInRow,
                            numberOfBillsPerPage
                          ),
                          numberOfBillsPerPage
                        );
                        this.props.setTableOfBillsCurrentPage(
                          paginationItems[paginationItems.length - 1].props
                            .children
                        );
                      }}
                    />
                  )}
                  {tableOfBills.currentPage < numberOfPages && (
                    <Pagination.Next
                      onClick={() => {
                        if (tableOfBills.currentPage < numberOfPages) {
                          this.props.fetchBills(
                            user.userContract.userContractId,
                            calculatePaginationOffset(
                              tableOfBills.currentPage + 1,
                              numberOfBillsPerPage
                            ),
                            numberOfBillsPerPage
                          );
                          this.props.setTableOfBillsCurrentPage(
                            tableOfBills.currentPage + 1
                          );
                        }
                      }}
                    />
                  )}
                  {tableOfBills.currentPage < numberOfPages && (
                    <Pagination.Last
                      onClick={() => {
                        this.props.fetchBills(
                          user.userContract.userContractId,
                          calculatePaginationOffset(
                            numberOfPages,
                            numberOfBillsPerPage
                          ),
                          numberOfBillsPerPage
                        );
                        this.props.setTableOfBillsCurrentPage(numberOfPages);
                      }}
                    />
                  )}
                </Pagination>
              )}
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
      fetchUserByContractId,
      fetchBills,
      setTableOfBillsVisibility,
      setTableOfBillsCurrentPage,
      setBillModalVisibility,
      fetchSelectedBill
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(TableOfBills);
