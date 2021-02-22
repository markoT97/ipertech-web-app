import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import Table from "../components/Table";
import fetchTvPackets from "../redux/actions/tvPacketsActions/actionCreators";
import { BACKEND_URL } from "../shared/constants";

const TvPackets = () => {
  const tvPackets = useSelector((state) => state.tvPackets);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchTvPackets());
  }, [dispatch]);

  const columns = {
    name: { title: "Naziv", highlighted: true },
    tvChannels: {
      title: "Kanali",
      nestedComponent: {
        positionNumber: { title: "Pozicija" },
        imageLocation: { title: "Logo", image: true },
        name: { title: "Naziv kanala" },
        tvBackwards: { title: "TV unazad", icon: true },
      },
    },
    price: { title: "Cena", highlighted: true },
  };

  const nestedComponentProperty = Object.keys(columns).filter(
    (columnKey) => columns[columnKey].nestedComponent
  );

  return (
    <div>
      <Table
        keysColumn={"tvPacketId"}
        title={"TV Paketi"}
        imagePoster={`${BACKEND_URL}/packets/tv/cover-photo.jpg`}
        columns={columns}
        data={tvPackets}
      >
        <Table
          keysColumn={"tvChannelId"}
          columns={columns[nestedComponentProperty].nestedComponent}
          data={tvPackets.map((tvPacket) => tvPacket.tvChannels)}
          hover
        />
      </Table>
    </div>
  );
};

export default TvPackets;
