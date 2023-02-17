import {Input} from "@mui/joy";
import Button from "@mui/joy/Button";
import {CssVarsProvider} from "@mui/joy/styles";
import MailIcon from '@mui/icons-material/Mail';
import dayjs, { Dayjs } from 'dayjs';
import TextField from '@mui/material/TextField';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import React from "react";
import "./CreateForm.css"
import {DelayedMessageService} from "../../services";
import {useNavigate} from "react-router-dom";

const messageService = new DelayedMessageService();

const tg = window.Telegram.WebApp;

export default function  CreateForm(){
    const [data,setData] = React.useState({
        text :"",
        timeToSend: new Date().toISOString().slice(0, 10)
    })
    
    const navigate = useNavigate();
    
    const handleSubmit = (event) =>{
        event.preventDefault();
        messageService.createMessage(data.text,tg.initDataUnsafe.user.id, data.timeToSend).then(res =>{
                navigate(-1);
        })
    }
    
    const onDateChange = (newValue) =>{
        setData((prev) => ({
            ...prev,
            timeToSend: newValue
        }))
    }

    const onTextChange = (newValue) =>{
        setData((prev) => ({
            ...prev,
            text :newValue
        }))
    }
    return (
        <div>
            <CssVarsProvider/>
            <form
                className={"createForm"}
                onSubmit={handleSubmit}
            >
                <Input startDecorator={<MailIcon />}
                       placeholder="Type in here…"
                       onChange={onTextChange}
                />
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <DateTimePicker
                        renderInput={(props) => <TextField {...props} />}
                        label="DateTimePicker"
                        value={data.timeToSend}
                        onChange={onDateChange}
                    />
                </LocalizationProvider>
                <Button type="submit">Submit</Button>
            </form>
        </div>
       
    );
    
}