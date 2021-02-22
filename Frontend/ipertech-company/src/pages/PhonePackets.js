import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import Table from "../components/Table";
import fetchPhonePackets from "../redux/actions/phonePacketsActions/actionCreators";
import { BACKEND_URL } from "../shared/constants";

const PhonePackets = () => {
  const phonePackets = useSelector((state) => state.phonePackets);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchPhonePackets());
  }, [dispatch]);

  const columns = {
    name: { title: "Naziv paketa", highlighted: true, uppercase: true },
    freeMinutes: {
      title: "Besplatni minuti",
    },
    price: { title: "Cena", highlighted: true },
  };

  return (
    <div>
      <Table
        keysColumn={"phonePacketId"}
        title={"Telefonski Paketi"}
        imagePoster={`${BACKEND_URL}/packets/phone/cover-photo.jpg`}
        columns={columns}
        data={phonePackets}
        hover
      ></Table>
    </div>
  );
};

export default PhonePackets;
