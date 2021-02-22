import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import Table from "../components/Table";
import fetchInternetPackets from "../redux/actions/internetPacketsActions/actionCreators";
import { BACKEND_URL } from "../shared/constants";

const InternetPackets = () => {
  const internetPackets = useSelector((state) => state.internetPackets);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchInternetPackets());
  }, []);

  const columns = {
    name: { title: "Naziv", highlighted: true, uppercase: true },
    speed: { title: "Brzina" },
    "internetRouter.imageLocation": { title: "Ruter", image: true },
    price: { title: "Cena" },
  };

  return (
    <div>
      <Table
        keysColumn={"internetPacketId"}
        title={`Internet paketi`}
        imagePoster={`${BACKEND_URL}/packets/internet/cover-photo.jpg`}
        columns={columns}
        data={internetPackets}
        hover
      />
    </div>
  );
};

export default InternetPackets;
