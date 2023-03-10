import {useEffect} from "react";
import './App.css';
import {
    BrowserRouter,
    Route,
    Navigate,
    Routes
} from 'react-router-dom';
import {Menu, CreateForm} from "./components";
import {DelayedMessages} from "./containers"


const tg = window.Telegram.WebApp;
export default function App() {
    useEffect(() => {
        tg.ready();
    }, [])
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/menu" element={<Menu/>}/>
                <Route path="/delayedMessages" element={<DelayedMessages/>}/>
                <Route path="/createForm" element={<CreateForm/>}/>
                <Route exact
                       path="/"
                       element={<Navigate replace to="/menu"/>}
                />
            </Routes>
        </BrowserRouter>
    );
}