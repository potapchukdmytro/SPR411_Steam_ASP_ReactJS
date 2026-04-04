import axios from "axios";
import {env} from "./env.js";
import {getCookie} from "./helpres/cookie.js";

export const api = axios.create({
    baseURL: env.api
});

api.interceptors.request.use(cfg => {
   const jwtToken = getCookie("jwt");
   if(jwtToken){
       cfg.headers.authorization = `Bearer ${jwtToken}`;
   }
   return cfg;
});