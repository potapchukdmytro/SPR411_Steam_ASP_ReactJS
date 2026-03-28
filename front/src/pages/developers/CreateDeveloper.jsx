import {useState, useRef} from "react";
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import FormLabel from '@mui/material/FormLabel';
import FormControl from '@mui/material/FormControl';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import Stack from '@mui/material/Stack';
import MuiCard from '@mui/material/Card';
import {styled} from '@mui/material/styles';
import {api} from "../../api.js";
import {toast} from "react-toastify";
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import {useNavigate} from "react-router";
import ClearIcon from '@mui/icons-material/Clear';
import CancelIcon from '@mui/icons-material/Cancel';
import {IconButton} from "@mui/material";

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

const Card = styled(MuiCard)(({theme}) => ({
    display: 'flex',
    flexDirection: 'column',
    alignSelf: 'center',
    width: '100%',
    padding: theme.spacing(4),
    gap: theme.spacing(2),
    margin: 'auto',
    [theme.breakpoints.up('sm')]: {
        maxWidth: '450px',
    },
    boxShadow:
        'hsla(220, 30%, 5%, 0.05) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.05) 0px 15px 35px -5px',
    ...theme.applyStyles('dark', {
        boxShadow:
            'hsla(220, 30%, 5%, 0.5) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.08) 0px 15px 35px -5px',
    }),
}));

const FormContainer = styled(Stack)(({theme}) => ({
    minHeight: '100%',
    padding: theme.spacing(2),
    [theme.breakpoints.up('sm')]: {
        padding: theme.spacing(4),
    },
    '&::before': {
        content: '""',
        display: 'block',
        position: 'absolute',
        zIndex: -1,
        inset: 0,
        backgroundImage:
            'radial-gradient(ellipse at 50% 50%, hsl(210, 100%, 97%), hsl(0, 0%, 100%))',
        backgroundRepeat: 'no-repeat',
        ...theme.applyStyles('dark', {
            backgroundImage:
                'radial-gradient(at 50% 50%, hsla(210, 100%, 16%, 0.5), hsl(220, 30%, 5%))',
        }),
    },
}));

const CreateDeveloper = () => {
    const [image, setImage] = useState(null);
    const nameRef = useRef(null);
    const imageRef = useRef(null);
    const navigate = useNavigate();
    const [imageHover, setImageHover] = useState(false);

    const submitHandler = async (event) => {
        event.preventDefault();
        const name = nameRef.current.value;

        const formData = new FormData();
        formData.append('name', name);
        if (image) {
            formData.append('image', image);
        }

        try {
            const response = await api.post("developer", formData);
            const {data} = response;
            toast.success(data.message);
            navigate("/developers");
        } catch (error) {
            const {data} = error.response;
            if (data.message) {
                toast.error(data.message);
            } else if (data.errors) {
                console.log(data.errors);
                toast.error(data.errors["Name"][0]);
            }
        }

    }

    const uploadHandler = (event) => {
        if (event.target.files[0]) {
            setImage(event.target.files[0]);
        }
    }

    const deleteImageHandler = () => {
        setImage(null);
        setImageHover(false);
        if(imageRef.current) {
            imageRef.current.value = null;
        }
    }

    return (
        <>
            <CssBaseline enableColorScheme/>
            <FormContainer direction="column" justifyContent="space-between">
                <Card variant="outlined">
                    <Typography
                        component="h1"
                        variant="h4"
                        sx={{width: '100%', fontSize: 'clamp(2rem, 10vw, 2.15rem)'}}
                    >
                        Новий розробник
                    </Typography>
                    <Box
                        component="form"
                        onSubmit={submitHandler}
                        noValidate
                        sx={{
                            display: 'flex',
                            flexDirection: 'column',
                            width: '100%',
                            gap: 2,
                        }}
                    >
                        <FormControl>
                            <FormLabel htmlFor="name">Назва</FormLabel>
                            <TextField
                                inputRef={nameRef}
                                id="name"
                                type="text"
                                name="name"
                                placeholder="Ім'я розробника"
                                autoComplete="name"
                                autoFocus
                                required
                                fullWidth
                                variant="outlined"
                            />
                        </FormControl>
                        <Box width="100%">
                            <FormLabel htmlFor="name">Зображення</FormLabel>
                            <Button
                                fullWidth
                                component="label"
                                role={undefined}
                                variant="contained"
                                tabIndex={-1}
                                startIcon={<CloudUploadIcon/>}
                            >
                                Обрати зобаження
                                <VisuallyHiddenInput
                                    type="file"
                                    accept="image/*"
                                    ref={imageRef}
                                    onChange={uploadHandler}
                                />
                            </Button>
                        </Box>
                        {
                            image && <Box sx={{position: 'relative'}}
                                          onMouseEnter={() => setImageHover(true)}
                                          onMouseLeave={() => setImageHover(false)}>
                                <Box
                                    component="img"
                                    height="300px"
                                    width="100%"
                                    sx={{objectFit: 'contain', opacity: imageHover ? 0.5 : 1}}
                                    src={URL.createObjectURL(image)}
                                    alt="preview"
                                />
                                {
                                    imageHover && <Box sx={{
                                        display: 'inline-block',
                                        position: 'absolute',
                                        top: "50%",
                                        left: "50%",
                                        transform: "translate(-50%, -50%)"
                                    }}>
                                    <IconButton onClick={deleteImageHandler}>
                                        <CancelIcon sx={{fontSize: "2em"}} color="error"/>
                                    </IconButton>
                                    </Box>
                                }

                            </Box>
                        }

                        <Button
                            sx={{mt: 1}}
                            type="submit"
                            fullWidth
                            variant="contained"
                        >
                            Зберегти
                        </Button>
                    </Box>
                </Card>
            </FormContainer>
        </>
    )
}

export default CreateDeveloper