import {useState, useEffect} from "react";
import {Box, List, ListItemButton, ListItemText} from "@mui/material";
import DelayedMessageService from "../services/DelayedMessageService";

const tg = window.Telegram.WebApp;
const delayedMessageService = new DelayedMessageService();
export default function DelayedMessageList() {
    const [delayedMessages, setDelayedMessages] = useState([]);
    const getItemsAsync = async (id) => {
        const call = () => fetch('https://localhost:44392/api/DelayedMessage/GetAllMessagesByUserId/' + id, {
            method: "get",
        });
        let res =  await call()
        return res;
    }

    useEffect( () => {
        const userId  = tg.initDataUnsafe.user.id
        delayedMessageService.getMessagesByUserId(userId).then(res => res.json()).then(
            res => {
                setDelayedMessages(res)
            }
        ).catch(
            res => {
                tg.showAlert("Something went wrong")
            }
        )
        // async function fetchMyAPI() {
        //     let response = await getItemsAsync(id)
        //     response = await response.json()
        //     setDelayedMessages(response)
        // }
        // fetchMyAPI()
        // tg.showAlert(delayedMessages)
    }, [])
    

    const listItems = delayedMessages.map((message) =>
        <ListItemButton key={message.id} >
            <ListItemText primary={message.text} style={{color:"green"}}/>
        </ListItemButton>
    );
    return (
        <div>
            <Box sx={{width: '100%', maxWidth: 360, bgcolor: 'background.paper'}}>
                <List component="nav" aria-label="secondary mailbox folder">
                    {listItems}
                </List>
            </Box>
        </div>
       
        
    );
}