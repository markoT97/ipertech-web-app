import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import Table from "../components/Table";
import { fetchPacketCombinations } from "../redux/actions/packetCombinationsActions/actionCreators";
import { BACKEND_URL } from "../shared/constants";

const PacketCombinations = () => {
  const packetCombinations = useSelector((state) => state.packetCombinations);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchPacketCombinations());
  }, [dispatch]);

  const columns = {
    name: { title: "Naziv paketa", highlighted: true, uppercase: true },
    "internetPacket.name": {
      title: "Internet",
    },
    "tvPacket.name": {
      title: "Televizija",
    },
    "phonePacket.name": {
      title: "Telefonija",
    },
    price: {
      title: "Cena",
      highlighted: true,
      total: packetCombinations.map(
        ({ internetPacket, tvPacket, phonePacket }) =>
          (
            internetPacket.price +
            (tvPacket?.price ?? 0) +
            (phonePacket?.price ?? 0)
          ).toFixed(2)
      ),
    },
  };

  return (
    <div>
      <Table
        keysColumn={"packetCombinationId"}
        title={"Kombinacije paketa"}
        imagePoster={`${BACKEND_URL}/packets/cover-photo.jpg`}
        columns={columns}
        data={packetCombinations}
        hover
      ></Table>
    </div>
  );
};

export default PacketCombinations;
