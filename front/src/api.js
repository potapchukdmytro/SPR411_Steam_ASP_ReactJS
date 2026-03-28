import axios from "axios";
import {env} from "./env.js";

export const api = axios.create({
    baseURL: env.api
})