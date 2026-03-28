import {Box, CircularProgress, Grid, IconButton} from "@mui/material";
import DeveloperCard from "../../components/cards/DeveloperCard.jsx";
import {useEffect, useState} from "react";
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import {api} from "../../api.js";
import {Link} from "react-router";

const DevelopersHome = () => {
    const [developers, setDevelopers] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        api.get("developer")
            .then(response => {
                const {data} = response;
                setDevelopers(data.payload);
                setLoading(false);
            })
            .catch(err => console.log(err));
    }, [])

    if (loading) return (
        <Box marginTop={5} sx={{display: "flex", justifyContent: "center"}}>
            <CircularProgress enableTrackSlot size="3rem"/>
        </Box>
    );

    return (
        <Box>
            <Grid container spacing={2} sx={{my: 2}}>
                {
                    developers.map((developer) => (
                        <Grid size={3} key={developer.id}>
                            <DeveloperCard developer={developer}/>
                        </Grid>
                    ))
                }
                <Grid size={3} sx={{textAlign: "center"}}>
                    <Link to="create">
                        <IconButton>
                            <AddCircleOutlineIcon sx={{fontSize: "5em;"}}/>
                        </IconButton>
                    </Link>
                </Grid>
            </Grid>
        </Box>
    )
}

export default DevelopersHome;