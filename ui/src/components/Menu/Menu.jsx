import {useNavigate} from "react-router-dom";
import {Button} from "@mui/material";
import "./Menu.css"

export default function Menu() {
    const navigate = useNavigate();

    const handleRemindersClick = () => {
        navigate('/delayedMessages')
    }

return (
        <div>
            <h1>Menu</h1>
            <div  id="parent" style={{flexDirection:"column", display:"flex", alignItems:"center", justifyContent:"space-between"}}>
                <Button onClick={handleRemindersClick} variant="contained">Мої нагадування</Button>
                <Button variant="contained">Something else</Button>
            </div>
        </div>
    )
}