import React, { Component } from "react";
import { connect } from "react-redux";
import { Table, Button } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import {
  fetchUserContracts,
  deleteUserContract,
} from "../../redux/actions/userContractsActions/actionCreators";
import { setInsertUserContractModalVisibility } from "./../../redux/actions/modalsActions/actionCreators";

export class ContractsTable extends Component {
  componentDidMount() {
    this.props.fetchUserContracts();
  }
  render() {
    const { userContracts } = this.props;
    return (
      <Table striped size="sm" className="text-center" responsive>
        <thead>
          <tr className="text-uppercase">
            <th>ID ugovora</th>
            <th>Kombinacija paketa</th>
            <th>Trajanje</th>
          </tr>
        </thead>
        <tbody>
          {userContracts.map((contract, i) => {
            return (
              <tr key={i}>
                <td className="text-uppercase">{contract.userContractId}</td>
                <td>{contract.packetCombination.name}</td>
                <td>{contract.contractDurationMonths}</td>

                <td>
                  <Button
                    variant="outline-danger"
                    onClick={() => this.props.deleteUserContract(contract)}
                  >
                    <Icon.Trash size={20} />
                  </Button>
                </td>
              </tr>
            );
          })}
          <tr>
            <td colSpan="4">
              <Button
                variant="success"
                className="text-center"
                onClick={() =>
                  this.props.setInsertUserContractModalVisibility(true)
                }
              >
                <Icon.Plus size={20} />
              </Button>
            </td>
          </tr>
        </tbody>
      </Table>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    userContracts: state.userContracts,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    {
      fetchUserContracts,
      setInsertUserContractModalVisibility,
      deleteUserContract,
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(ContractsTable);
