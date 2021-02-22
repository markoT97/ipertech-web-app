import * as yup from "yup";
import { userContractDurations } from "./constants";

yup.setLocale({
  mixed: {
    required: "Popunite polje da biste nastavili",
  },
  string: {
    email: "Upišite validnu imejl adresu",
    min: ({ min }) => `Vrednost je prekratka (minimum ${min} karaktera)`,
    max: ({ max }) => `Vredunost je predugačka (maksimum ${max} karaktera)`,
  },
  number: {
    min: ({ min }) =>
      `Vrednost nije validna (mora biti manja ili jednaka ${min})`,
    max: ({ max }) =>
      `Vrednost nije validna (mora biti veća ili jednaka ${max})`,
    integer: () => "Vrednost mora biti broj",
  },
});

export const loginValidationSchema = yup.object({
  email: yup.string().email().required(),
  password: yup.string().required(),
});

export const registerValidationSchema = yup.object({
  userContractId: yup.string().required(),
  firstName: yup.string().required(),
  lastName: yup.string().required(),
  gender: yup.string().required(),
  email: yup.string().email().required(),
  phoneNumber: yup.string().required(),
  password: yup.string().min(8).required(),
  passwordConfirm: yup.string().min(8).required(),
});

export const insertMessageValidationSchema = yup.object({
  title: yup.string().min(10).max(15).required(),
  content: yup.string().min(30).max(100).required(),
});

export const changePasswordValidationSchema = yup.object({
  password: yup.string().required(),
  passwordConfirmation: yup
    .string()
    .oneOf([yup.ref("password")], "Lozinke se moraju poklapati")
    .required(),
});

export const insertPacketCombinationValidationSchema = yup.object({
  name: yup.string().min(5).max(50).required(),
  internetPacket: yup.string().required(),
  tvPacket: yup.string(),
  phonePacket: yup.string(),
});

const contractDurations = userContractDurations.map(
  (contract) => contract.contractDurationMonths
);

export const insertUserContractValidationSchema = yup.object({
  packetCombination: yup.string().required(),
  duration: yup
    .number()
    .oneOf(
      contractDurations,
      `Trajanje ugovora može biti ${contractDurations}`
    ),
});

export const insertNewsValidationSchema = yup.object({
  title: yup.string().min(10).max(15).required(),
  content: yup.string().min(30).max(100).required(),
  image: yup
    .mixed()
    .required()
    .test("imageFormat", "Izaberite sliku formata .png ili .jpeg", (value) => {
      return (
        value && (value.type === "image/jpeg" || value.type === "image/png")
      );
    }),
});
