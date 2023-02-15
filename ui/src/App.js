import {useEffect} from "react";
import './App.css';
import {
  BrowserRouter,
  Route,
  Navigate,
  Routes
} from 'react-router-dom';
import Menu from "./components/Menu";
import DelayedMessageList from "./components/DelayedMessageList/DelayedMessageList";


const tg = window.Telegram.WebApp;
export default function App() {
  useEffect(() => {
    tg.ready();
  }, [])
  return (
      <BrowserRouter>
        <Routes>
          <Route path="/menu" element={<Menu />}  />
            <Route path="/delayedMessages" element={<DelayedMessageList/>}  />
          <Route exact
                 path="/"
                 element={<Navigate replace to="/menu" />}
          />
        </Routes>
      </BrowserRouter>
  );
}