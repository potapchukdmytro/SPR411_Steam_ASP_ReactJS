import { useState, useEffect, use } from "react";
import "./App.css";
import axios from "axios";
import { Button } from "@mui/material";
import { useRef } from "react";

function App() {
    const [genreId, setGenreId] = useState(0);
    const [genres, setGenres] = useState([]);
    const inputRef = useRef(null);

    useEffect(() => {
        const fetchGenres = async () => {
            if (genreId != 0) {
                const url = `https://localhost:7057/api/genre/${genreId}`;
                const response = await axios.get(url);
                if (response.status == 200) {
                    setGenres([response.data]);
                }
            } else {
                const url = "https://localhost:7057/api/genre";
                const response = await axios.get(url);
                if (response.status == 200) {
                    setGenres(response.data);
                }
            }
        };

        fetchGenres();
    }, [genreId]);

    const findGenreHandle = () => {
        const value = inputRef.current.value;
        setGenreId(value);
    };

    return (
        <>
            <div>
                <input
                    min={0}
                    defaultValue={0}
                    ref={inputRef}
                    name="genreId"
                    type="number"
                />
                <Button onClick={findGenreHandle} variant="contained">
                    Пошук по id
                </Button>
                <ul>
                    {genres.map((g) => (
                        <li key={g.id}>
                            {g.id} - {g.name}
                        </li>
                    ))}
                </ul>
            </div>
        </>
    );
}

export default App;
