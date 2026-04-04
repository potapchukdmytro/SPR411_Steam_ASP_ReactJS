import "./App.css";
import {Route, Routes} from "react-router";
import DefaultLayout from "./components/layouts/DefaultLayout.jsx";
import DevelopersHome from "./pages/developers/DevelopersHome.jsx";
import {darkTheme} from "./theming/darkTheme.js";
import {ThemeProvider} from "@mui/material";
import CreateDeveloper from "./pages/developers/CreateDeveloper.jsx";
import {Bounce, ToastContainer} from "react-toastify";
import NotFound from "./pages/notFound/NotFound.jsx";
import CssBaseline from "@mui/material/CssBaseline";
import Login from "./pages/auth/login/Login.jsx";
import {useDispatch} from "react-redux";
import {useEffect} from "react";
import {getCookie} from "./helpres/cookie.js";
import {jwtDecode} from "jwt-decode";

function App() {
    const dispatch = useDispatch();

    useEffect(() => {
        const jwtToken = getCookie("jwt");
        if (jwtToken) {
            const payload = jwtDecode(jwtToken);
            const user = {
                userName: payload.userName,
                email: payload.email,
                image: payload.image,
                firstName: payload.firstName,
                lastName: payload.lastName,
            };
            dispatch({type: "LOGIN", payload: user});
        }
    }, [])

    return (
        <>
            <ThemeProvider theme={darkTheme}>
                <CssBaseline/>
                <Routes>
                    <Route path="/" element={<DefaultLayout/>}>
                        <Route path="developers">
                            <Route index element={<DevelopersHome/>}/>
                            <Route path="create" element={<CreateDeveloper/>}/>
                        </Route>
                        <Route path="login" element={<Login/>}/>
                        <Route path="*" element={<NotFound/>}/>
                    </Route>
                </Routes>
            </ThemeProvider>
            <ToastContainer
                position="top-right"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick={false}
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="dark"
                transition={Bounce}
            />
        </>
    );
}

export default App;
