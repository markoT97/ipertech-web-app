import React, { Component } from "react";
import { Button, Table } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { fetchUserById } from "./../../redux/actions/userActions/actionCreators";

export class UserData extends Component {
  render() {
    const { user } = this.props;
    return (
      <Table striped responsive className="text-center">
        <thead>
          <tr>
            <th colSpan={4} className="bg-success text-white">
              <Icon.PersonFill size={35} />
              Podaci
            </th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td colSpan={2} className="align-middle">
              ID ugovora:
            </td>
            <td colSpan={2}>{user.userContract.userContractId}</td>
          </tr>
          <tr>
            <td colSpan={2}>Ime i prezime:</td>
            <td colSpan={2}>{user.firstName + " " + user.lastName}</td>
          </tr>
          <tr>
            <td colSpan={2}>Imejl adresa:</td>
            <td colSpan={2}>{user.email}</td>
          </tr>
          <tr>
            <td rowSpan={3} className="text-center align-middle">
              <Icon.Folder size={70} />
            </td>
            <td className="align-middle">
              <Icon.At size={25} />
              &nbsp; Internet
            </td>
            <td className="text-danger align-middle">
              {user.userContract.packetCombination.internetPacket.name}
            </td>
            <td className="align-middle">
              <Button variant="outline-primary mb-2">
                <Icon.Pencil size={25} />
              </Button>
            </td>
          </tr>
          <tr>
            <td className="align-middle">
              <Icon.Tv size={25} />
              &nbsp; Televizija
            </td>
            <td className="text-danger align-middle">
              {user.userContract.packetCombination.tvPacket
                ? user.userContract.packetCombination.tvPacket.name
                : ""}
            </td>
            <td className="align-middle">
              <Button variant="outline-primary mb-2">
                <Icon.Pencil size={25} />
              </Button>
              {user.userContract.packetCombination.tvPacket ? (
                <Button variant="outline-danger">
                  <Icon.XCircle size={25} />
                </Button>
              ) : (
                ""
              )}
            </td>
          </tr>
          <tr>
            <td className="align-middle">
              <Icon.Phone size={25} />
              &nbsp; Telefonija
            </td>
            <td className="text-danger align-middle">
              {user.userContract.packetCombination.phonePacket
                ? user.userContract.packetCombination.phonePacket.name
                : ""}
            </td>
            <td className="align-middle">
              <Button variant="outline-primary mb-2">
                <Icon.Pencil size={25} />
              </Button>
              {user.userContract.packetCombination.phonePacket ? (
                <Button variant="outline-danger">
                  <Icon.XCircle size={25} />
                </Button>
              ) : (
                ""
              )}
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
    user: state.user
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchUserById }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(UserData);
