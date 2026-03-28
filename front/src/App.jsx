import "./App.css";
import {Route, Routes} from "react-router";
import DefaultLayout from "./components/layouts/DefaultLayout.jsx";
import DevelopersHome from "./pages/developers/DevelopersHome.jsx";
import {darkTheme} from "./theming/darkTheme.js";
import {ThemeProvider} from "@mui/material";
import CreateDeveloper from "./pages/developers/CreateDeveloper.jsx";
import {Bounce, ToastContainer} from "react-toastify";
import NotFound from "./pages/notFound/NotFound.jsx";

function App() {

    return (
        <>
            <ThemeProvider theme={darkTheme}>
                <Routes>
                    <Route path="/" element={<DefaultLayout/>}>
                        <Route path="developers">
                            <Route index element={<DevelopersHome/>}/>
                            <Route path="create" element={<CreateDeveloper/>}/>
                        </Route>
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
