import {Card, CardContent, CardMedia, Typography} from "@mui/material";

const DeveloperCard = ({developer}) => {
    return (
        <Card sx={{maxWidth: 345}}>
            <CardMedia
                sx={{height: 300}}
                image="https://blog.juanxxiii.com/application/modules/themes/views/default/assets/images/image-placeholder.png"
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