import {useRef, useState} from "react";
import {Box, Button, InputLabel, styled, TextField} from "@mui/material";
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import {api} from "../../api.js";
import {toast} from "react-toastify";

const VisuallyHiddenInput = styled('input')({
    clip: 'rect(0 0 0 0)',
    clipPath: 'inset(50%)',
    height: 1,
    overflow: 'hidden',
    position: 'absolute',
    bottom: 0,
    left: 0,
    whiteSpace: 'nowrap',
    width: 1,
});

const CreateDeveloper = () => {
    const [image, setImage] = useState(null);
    const nameRef = useRef(null);

    const submitHandler = async (event) => {
        event.preventDefault();
        const name = nameRef.current.value;

        const formData = new FormData();
        formData.append('name', name);
        formData.append('image', image);

        try {
        const response = await api.post("developer", formData);
            const {data} = response;
            toast.success(data.message);
        } catch (error) {
            const {data} = error.response;
            toast.error(data.message);
        }

    }

    const uploadHandler = (event) => {
        if(event.target.files[0]){
            setImage(event.target.files[0]);
        }
    }

    return (
        <Box width="50%" mt={3} mx="auto" p={4} sx={{border: "5px double grey", backgroundColor: "#757575"}}>
            <h1 style={{color: "white"}} align="center">Створення нового розробника</h1>
            <Box onSubmit={submitHandler} component="form"
                 sx={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
                <Box width="100%" m={1}>
                    <InputLabel>Ім'я</InputLabel>
                    <TextField
                        fullWidth
                        required
                        id="standard-required"
                        inputRef={nameRef}
                        variant="outlined"
                    />
                </Box>
                <Box width="100%" m={1}>
                    <InputLabel>Зображення</InputLabel>
                    <Button
                        fullWidth
                        component="label"
                        role={undefined}
                        variant="contained"
                        tabIndex={-1}
                        startIcon={<CloudUploadIcon/>}
                    >
                        Upload file
                        <VisuallyHiddenInput
                            type="file"
                            accept="image/*"
                            onChange={uploadHandler}
                        />
                    </Button>
                </Box>
                <Box width="100%" m={1}>
                    <Button fullWidth variant='contained' type="submit">
                        Створити
                    </Button>
                </Box>
            </Box>
        </Box>
    )
}

export default CreateDeveloper