import {combineReducers} from "@reduxjs/toolkit";
import {authReducer} from "./authReducer.js";

export const rootReducer = combineReducers({
    auth: authReducer
})