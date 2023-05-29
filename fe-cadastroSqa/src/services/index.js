import axios from "axios";
import UsersService from "./users";

export const httpClient = axios.create({
  baseURL: "http://localhost:8080",
  headers:{
    "Content-Type": "application/json"
  }
});

export default {
  users: UsersService(httpClient)
}