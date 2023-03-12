import {useEffect} from "react";
import './App.css';
import {
    BrowserRouter,
    Route,
    Navigate,
    Routes
} from 'react-router-dom';
import {Menu, CreateDelayedMessageForm,CreateRecurringMessageForm} from "./components";
import {DelayedMessages,RecurringMessages} from "./containers"


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
                <Route path="/recurringMessages" element={<RecurringMessages/>}/>
                <Route path="/createDelayedMessage" element={<CreateDelayedMessageForm/>}/>
                <Route path="/createRecurringMessage" element={<CreateRecurringMessageForm/>}/>
                <Route exact
                       path="/"
                       element={<Navigate replace to="/menu"/>}
                />
            </Routes>
        </BrowserRouter>
    );
}