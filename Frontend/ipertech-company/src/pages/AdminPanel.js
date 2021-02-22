import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import FormModal from "../components/FormModal";
import Table from "../components/Table";
import * as Icon from "react-bootstrap-icons";
import fetchInternetPackets from "../redux/actions/internetPacketsActions/actionCreators";
import {
  setInsertNewsModalVisibility,
  setInsertPacketCombinationModalVisibility,
  setInsertUserContractModalVisibility,
} from "../redux/actions/modalsActions/actionCreators";
import {
  deletePacketCombination,
  fetchPacketCombinations,
  insertPacketCombination,
} from "../redux/actions/packetCombinationsActions/actionCreators";
import fetchPhonePackets from "../redux/actions/phonePacketsActions/actionCreators";
import fetchTvPackets from "../redux/actions/tvPacketsActions/actionCreators";
import {
  insertNewsValidationSchema,
  insertPacketCombinationValidationSchema,
  insertUserContractValidationSchema,
} from "../shared/validation-schemas";
import {
  deleteUserContract,
  fetchUserContracts,
  insertUserContract,
} from "../redux/actions/userContractsActions/actionCreators";
import { userContractDurations } from "../shared/constants";
import {
  deleteNews,
  fetchNews,
  insertNews,
} from "../redux/actions/newsActions/actionCreators";
import { parseISO, format } from "date-fns";

const AdminPanel = () => {
  const packetCombinations = useSelector((state) => state.packetCombinations);
  const insertPacketCombinationModalVisibility = useSelector(
    (state) => state.modalsVisibility.insertPacketCombinationModalVisibility
  );

  const internetPackets = useSelector((state) => state.internetPackets);
  const tvPackets = useSelector((state) => state.tvPackets);
  const phonePackets = useSelector((state) => state.phonePackets);

  const dispatch = useDispatch();

  // Packet combinations

  useEffect(() => {
    dispatch(fetchPacketCombinations());
  }, [dispatch]);

  useEffect(() => {
    dispatch(fetchInternetPackets());
    dispatch(fetchTvPackets());
    dispatch(fetchPhonePackets());
  }, [dispatch]);

  const packetCombinationsColumns = {
    name: { title: "Naziv paketa" },
    "internetPacket.name": {
      title: "Internet",
    },
    "tvPacket.name": {
      title: "Televizija",
    },
    "phonePacket.name": {
      title: "Telefonija",
    },
  };

  const packetCombinationsFormData = [
    {
      controlId: "formPacketCombinationName",
      label: "Naziv",
      type: "text",
      name: "name",
      placeholder: "Unesite naziv paketa",
    },
    {
      select: {
        controlId: "formInternetPacketState",
        type: "select",
        label: "Internet",
        prepend: "@",
        name: "internetPacket",
        placeholder: "Izaberite internet paket",
        data: internetPackets,
        value: {
          internetPacketId: "internetPacketId",
          "internetRouter.internetRouterId": "internetRouterId",
        },
      },
    },
    {
      select: {
        controlId: "formTvPacketState",
        as: "select",
        label: "Televizija",
        prepend: <Icon.Tv />,
        name: "tvPacket",
        placeholder: "Izaberite tv paket",
        data: tvPackets,
        value: {
          tvPacketId: "tvPacketId",
        },
      },
    },
    {
      select: {
        controlId: "formPhonePacketState",
        type: "select",
        label: "Telefonija",
        prepend: <Icon.Phone />,
        name: "phonePacket",
        placeholder: "Izaberite telefonski paket",
        data: phonePackets,
        value: {
          phonePacketId: "phonePacketId",
        },
      },
    },
  ];

  const packetCombinationsCrud = {
    create: {
      action: () => dispatch(setInsertPacketCombinationModalVisibility(true)),
      content: <Icon.Plus size={20} />,
    },
    delete: {
      action: (packet) => dispatch(deletePacketCombination(packet)),
      content: <Icon.Trash size={20} />,
    },
  };

  // User contracts
  const contracts = useSelector((state) => state.userContracts);
  const insertUserContractModalVisibility = useSelector(
    (state) => state.modalsVisibility.insertUserContractModalVisibility
  );

  useEffect(() => {
    dispatch(fetchUserContracts());
  }, [dispatch]);

  const contractsColumns = {
    userContractId: { title: "ID ugovora" },
    "packetCombination.name": {
      title: "Kombinacija paketa",
    },
    contractDurationMonths: {
      title: "Trajanje",
    },
  };

  const contractsFormData = [
    {
      select: {
        controlId: "formInternetPacketState",
        type: "select",
        label: "Kombinacija paketa",
        prepend: <Icon.BoxArrowRight />,
        name: "packetCombination",
        placeholder: "Izaberite kombinaciju paketa",
        data: packetCombinations,
        value: {
          packetCombinationId: "packetCombinationId",
          name: "name",
        },
      },
    },
    {
      select: {
        controlId: "formUserContractName",
        type: "select",
        label: "Trajanje ugovora",
        name: "contractDurationMonths",
        placeholder: "Izaberite dužinu trajanja ugovora",
        data: userContractDurations,
        value: {
          contractDurationMonths: "contractDurationMonths",
        },
      },
    },
  ];

  const userContractsCrud = {
    create: {
      action: () => dispatch(setInsertUserContractModalVisibility(true)),
      content: <Icon.Plus size={20} />,
    },
    delete: {
      action: (contract) => dispatch(deleteUserContract(contract)),
      content: <Icon.Trash size={20} />,
    },
  };

  // News
  const news = useSelector((state) => state.news);
  const insertNewsModalVisibility = useSelector(
    (state) => state.modalsVisibility.insertNewsModalVisibility
  );

  useEffect(() => {
    dispatch(fetchNews());
  }, [dispatch]);

  const newsColumns = {
    title: { title: "Naslov" },
    content: {
      title: "Sadržaj",
    },
    createdAt: {
      title: "Kreirano",
      format: (value) => {
        return format(parseISO(value), "dd.MM.yyyy");
      },
    },
    imageLocation: {
      title: "Slika",
      image: true,
    },
  };

  const newsFormData = [
    {
      controlId: "formNewsName",
      label: "Naslov",
      name: "title",
      placeholder: "Unesite naslov novosti",
      value: {
        title: "title",
      },
    },
    {
      controlId: "formNewsContent",
      type: "text",
      label: "Sadržaj",
      name: "content",
      placeholder: "Unesite sadržaj novosti",
      value: {
        content: "content",
      },
    },
    {
      select: {
        controlId: "formNewsImage",
        type: "file",
        label: "Slika",
        prepend: <Icon.Image size={25} />,
        name: "image",
        placeholder: "Izaberite sliku",
        value: {
          image: "image",
        },
      },
    },
  ];

  const newsCrud = {
    create: {
      action: () => dispatch(setInsertNewsModalVisibility(true)),
      content: <Icon.Plus size={20} />,
    },
    delete: {
      action: (contract) => dispatch(deleteNews(contract)),
      content: <Icon.Trash size={20} />,
    },
  };

  return (
    <div>
      {/* Packet combinations */}
      <Table
        keysColumn={"packetCombinationId"}
        title={"Upravljanje paketima"}
        columns={packetCombinationsColumns}
        data={packetCombinations}
        crud={packetCombinationsCrud}
        striped
      />

      <FormModal
        modalTitle={"Dodavanje nove kombinacije paketa"}
        formData={packetCombinationsFormData}
        validationSchema={insertPacketCombinationValidationSchema}
        show={insertPacketCombinationModalVisibility}
        onHide={() =>
          dispatch(setInsertPacketCombinationModalVisibility(false))
        }
        onSubmit={(value) => dispatch(insertPacketCombination(value))}
        submitButtonText={"Dodaj"}
      />

      {/* Contracts */}
      <Table
        keysColumn={"userContractId"}
        title={"Upravljanje ugovorima korisnika"}
        columns={contractsColumns}
        data={contracts}
        crud={userContractsCrud}
        striped
      />

      <FormModal
        modalTitle={"Dodavanje nove kombinacije paketa"}
        formData={contractsFormData}
        validationSchema={insertUserContractValidationSchema}
        show={insertUserContractModalVisibility}
        onHide={() => dispatch(setInsertUserContractModalVisibility(false))}
        onSubmit={(value) => dispatch(insertUserContract(value))}
        submitButtonText={"Dodaj"}
      />

      {/* News */}
      <Table
        keysColumn={"notificationId"}
        title={"Upravljanje novostima"}
        columns={newsColumns}
        data={news}
        crud={newsCrud}
        striped
      />

      <FormModal
        modalTitle={"Dodavanje novosti"}
        formData={newsFormData}
        validationSchema={insertNewsValidationSchema}
        show={insertNewsModalVisibility}
        onHide={() => dispatch(setInsertNewsModalVisibility(false))}
        onSubmit={(value) => dispatch(insertNews(value))}
        submitButtonText={"Dodaj"}
      />
    </div>
  );
};

export default AdminPanel;
