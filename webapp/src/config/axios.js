import axios from "axios";

const Api_url = process.env.REACT_APP_API_URL;
const clienteAxios = axios.create({
  baseURL: Api_url,
});

export default clienteAxios;
