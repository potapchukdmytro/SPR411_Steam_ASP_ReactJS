import React from "react";
import {Box, Typography, Button, Container} from "@mui/material";
import {useNavigate} from "react-router";

const NotFound = () => {
    const navigate = useNavigate();

    return (
        <Container maxWidth="md">
            <Box
                sx={{
                    height: "100vh",
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                    justifyContent: "center",
                    textAlign: "center"
                }}
            >
                <Typography
                    variant="h1"
                    sx={{
                        fontSize: {xs: "6rem", md: "8rem"},
                        fontWeight: 700,
                        color: "primary.main"
                    }}
                >
                    404
                </Typography>

                <Typography variant="h5" sx={{mb: 2}}>
                    Сторінку не знайдено
                </Typography>

                <Typography variant="body1" color="text.secondary" sx={{mb: 4}}>
                    Можливо, сторінка була видалена або ви перейшли за неправильним посиланням.
                </Typography>

                <Button
                    variant="contained"
                    size="large"
                    onClick={() => navigate("/")}
                >
                    Повернутись на головну
                </Button>
            </Box>
        </Container>
    );
}

export default NotFound