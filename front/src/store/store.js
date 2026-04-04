import {configureStore} from "@reduxjs/toolkit";
import {rootReducer} from "./reducers/rootReducer.js";

export const store = configureStore({
    reducer: rootReducer
});