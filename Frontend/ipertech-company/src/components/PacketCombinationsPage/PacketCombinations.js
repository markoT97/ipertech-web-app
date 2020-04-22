import React, { Component } from "react";
import { Image, Table } from "react-bootstrap";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import "./../App.scss";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { fetchPacketCombinations } from "../../redux/actions/packetCombinationsActions/actionCreators";

export class Phone extends Component {
  componentDidMount() {
    this.props.fetchPacketCombinations();
  }
  render() {
    const { packetCombinations } = this.props;
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Kombinacije paketa</h5>

        <Image
          src={BACKEND_URL + "/packets/cover-photo.jpg"}
          className="page-cover"
        />

        <Table bordered hover responsive className="text-center">
          <thead className="text-uppercase">
            <tr>
              <th>Naziv paketa</th>
              <th>Internet</th>
              <th>Televizija</th>
              <th>Telefonija</th>
              <th>Cena</th>
            </tr>
          </thead>
          <tbody>
            {packetCombinations.map((pc, i) => {
              return (
                <tr key={i}>
                  <td className="align-middle text-danger text-uppercase">
                    {pc.name}
                  </td>
                  <td className="align-middle">{pc.internetPacket.name}</td>
                  <td className="align-middle text-danger">
                    {pc.tvPacket == null ? "-" : pc.tvPacket.name}
                  </td>
                  <td className="align-middle text-danger">
                    {pc.phonePacket == null ? "-" : pc.phonePacket.name}
                  </td>
                  <td className="align-middle text-danger">
                    {(
                      pc.internetPacket.price +
                      (!pc.tvPacket ? 0 : pc.tvPacket.price) +
                      (!pc.phonePacket ? 0 : pc.phonePacket.price)
                    ).toFixed(2)}
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    packetCombinations: state.packetCombinations,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({ fetchPacketCombinations }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Phone);
