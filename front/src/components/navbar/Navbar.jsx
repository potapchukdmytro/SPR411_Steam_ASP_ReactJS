import {AppBar, Box, Button, IconButton, Toolbar, Typography} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import {Link} from "react-router";
import {useDispatch, useSelector} from "react-redux";
import {removeCookie} from "../../helpres/cookie.js";

const Navbar = () => {
    const {user, isAuth} = useSelector((state) => state.auth);
    const dispatch = useDispatch();

    const logout = () => {
        dispatch({type: "LOGOUT"});
        removeCookie("jwt");
    }

    return (
        <Box sx={{flexGrow: 1}}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{mr: 2}}
                    >
                        <MenuIcon/>
                    </IconButton>
                    <Typography variant="h6" component="div" sx={{mx: 1}}>
                        Steam clone
                    </Typography>
                    <Link to="/games">
                        <Typography variant="h6" component="div" sx={{mx: 1}}>
                            Ігри
                        </Typography>
                    </Link>
                    <Link to="/developers">
                        <Typography variant="h6" component="div" sx={{mx: 1}}>
                            Розробники
                        </Typography>
                    </Link>
                    <Link to="/genres">
                        <Typography variant="h6" component="div" sx={{mx: 1}}>
                            Жанри
                        </Typography>
                    </Link>
                    <Box sx={{flexGrow: 1, textAlign: "right"}}>
                        {
                            isAuth ?
                                <>
                                    <Link to="/profile">
                                        <Button color="inherit">Вітаємо, {user.userName}</Button>
                                    </Link>
                                    <Button onClick={logout} color="inherit">Вийти</Button>
                                </>
                                :
                                <Link to="/login">
                                    <Button color="inherit">Login</Button>
                                </Link>
                        }

                    </Box>
                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default Navbar;