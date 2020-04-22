import React, { Component } from "react";
import { connect } from "react-redux";
import { Table, Button } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import {
  fetchPacketCombinations,
  deletePacketCombination,
} from "../../redux/actions/packetCombinationsActions/actionCreators";
import { setInsertPacketCombinationModalVisibility } from "./../../redux/actions/modalsActions/actionCreators";

export class PacketCombinations extends Component {
  componentDidMount() {
    this.props.fetchPacketCombinations();
  }
  render() {
    const { packetCombinations } = this.props;
    return (
      <Table striped size="sm" className="text-center" responsive>
        <thead>
          <tr className="text-uppercase">
            <th>Naziv paketa</th>
            <th>Internet</th>
            <th>Televizija</th>
            <th>Telefonija</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {packetCombinations.map((packet, i) => {
            return (
              <tr key={i}>
                <td className="text-danger text-uppercase">{packet.name}</td>
                <td>{packet.internetPacket.name}</td>
                <td>{packet.tvPacket ? packet.tvPacket.name : "-"}</td>
                <td>{packet.phonePacket ? packet.phonePacket.name : "-"}</td>
                <td>
                  <Button
                    variant="outline-danger"
                    onClick={() => this.props.deletePacketCombination(packet)}
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
                  this.props.setInsertPacketCombinationModalVisibility(true)
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
    packetCombinations: state.packetCombinations,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    {
      fetchPacketCombinations,
      setInsertPacketCombinationModalVisibility,
      deletePacketCombination,
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(PacketCombinations);
