import Delete from '@mui/icons-material/Delete';
import {CssVarsProvider} from '@mui/joy/styles';
import List from '@mui/joy/List';
import ListItem from '@mui/joy/ListItem';
import ListItemContent from '@mui/joy/ListItemContent';
import {IconButton} from "@mui/joy";
import "./DelayedMessageList.css"


export default function DelayedMessageList({delayedMessages}) {
    return (
        <div>
            <div className={"messageListContainer"}>
                <CssVarsProvider/>
                    <List sx={{maxWidth: 300}} size={"lg"}>
                        {
                            delayedMessages.map(message =>
                                <ListItem key={message.id} endAction={
                                    <IconButton aria-label="Delete" size="sm" color="danger">
                                        <Delete/>
                                    </IconButton>
                                }>
                                    <ListItemContent className={"text"}>{message.text} - {message.enqueueAt}</ListItemContent>
                                </ListItem>
                            )
                        }
                    </List>
            </div>        
        </div>


    );
}

