import {Box, CircularProgress, Grid} from "@mui/material";
import DeveloperCard from "../../components/cards/DeveloperCard.jsx";
import {useEffect, useState} from "react";
import {api} from "../../api.js";

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
            <Grid container spacing={2} marginTop={2}>
                {
                    developers.map((developer) => (
                        <Grid size={3} key={developer.id}>
                            <DeveloperCard developer={developer}/>
                        </Grid>
                    ))
                }
            </Grid>
        </Box>
    )
}

export default DevelopersHome;