import axios from "axios";
const Api_url = process.env.REACT_APP_API_URL;
let user = JSON.parse(localStorage.getItem("user"));
const clienteAxiosAuth = axios.create({
  baseURL: Api_url,
  headers: {
    Authorization: user ? user.TokenContent : "",
    "Content-Type": "application/json",
    Accept: "application/json"
  }
});

export default clienteAxiosAuth;
