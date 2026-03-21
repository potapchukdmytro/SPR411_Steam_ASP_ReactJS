import {Container} from "@mui/material";
import {Outlet} from "react-router";
import Navbar from "../navbar/Navbar.jsx";

const DefaultLayout = () => {
    return (
        <>
            <Navbar/>
            <Container>
                <Outlet/>
            </Container>
        </>
    )
}

export default DefaultLayout;