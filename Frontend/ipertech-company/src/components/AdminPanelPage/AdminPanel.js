import React, { Component } from "react";
import { connect } from "react-redux";
import PacketsTable from "./PacketsCombinationsTable";
import { bindActionCreators } from "redux";
import InsertPacketCombinationModal from "./InsertPacketCombinationModal";
import ContractsTable from "./ContractsTable";
import InsertUserContractModal from "./InsertUserContractModal";

export class AdminPanel extends Component {
  render() {
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Upravljanje paketima</h5>
        <PacketsTable />
        <InsertPacketCombinationModal />

        <h5 className="text-danger text-uppercase">
          Upravljanje ugovorima korisnika
        </h5>
        <ContractsTable />
        <InsertUserContractModal />
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
  return bindActionCreators({}, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(AdminPanel);
