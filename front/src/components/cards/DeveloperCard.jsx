import {Card, CardContent, CardMedia, Typography} from "@mui/material";
import {env} from "../../env.js";

const DeveloperCard = ({developer}) => {
    return (
        <Card sx={{maxWidth: 345}}>
            <CardMedia
                sx={{height: 300}}
                image={developer.image
                    ? env.developerImage + developer.image
                    : env.shareImage + "no-image.png"}
                title="green iguana"
            />
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    {developer.name}
                </Typography>
            </CardContent>
        </Card>
    )
}

export default DeveloperCard