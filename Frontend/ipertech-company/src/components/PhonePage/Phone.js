import React, { Component } from "react";
import { Image, Table } from "react-bootstrap";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchPhonePackets from "../../redux/actions/phonePacketsActions/actionCreators";

export class Phone extends Component {
  componentDidMount() {
    this.props.fetchPhonePackets();
  }
  render() {
    const { phonePackets } = this.props;
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Telefonski paketi</h5>

        <Image
          src={BACKEND_URL + "/packets/phone/cover-photo.jpg"}
          className="page-cover"
        />

        <Table bordered hover responsive className="text-center">
          <thead className="text-uppercase">
            <tr>
              <th>Naziv paketa</th>
              <th>Besplatni minuti</th>
              <th>Cena</th>
            </tr>
          </thead>
          <tbody>
            {phonePackets.map((pp, i) => {
              return (
                <tr key={i}>
                  <td className="align-middle text-danger text-uppercase">
                    {pp.name}
                  </td>
                  <td className="align-middle">{pp.freeMinutes}</td>
                  <td className="align-middle text-danger">{pp.price}</td>
                </tr>
              );
            })}
          </tbody>
        </Table>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    phonePackets: state.phonePackets
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchPhonePackets }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Phone);
