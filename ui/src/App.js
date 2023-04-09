import {useEffect} from "react";
import './App.css';
import {
    BrowserRouter,
    Route,
    Navigate,
    Routes
} from 'react-router-dom';
import {CreateEventForm, Events} from "./components";


const tg = window.Telegram.WebApp;
export default function App() {
    useEffect(() => {
        tg.ready();
    }, [])
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/events" element={<Events/>}/>
                <Route path="/createEvent" element={<CreateEventForm/>}/>
                <Route exact
                       path="/"
                       element={<Navigate replace to="/events"/>}
                />
            </Routes>
        </BrowserRouter>
    );
}