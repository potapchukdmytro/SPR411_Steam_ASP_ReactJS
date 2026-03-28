import {AppBar, Box, Button, IconButton, Toolbar, Typography} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import {Link} from "react-router";

const Navbar = () => {
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
                        <Button color="inherit">Login</Button>
                    </Box>
                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default Navbar;