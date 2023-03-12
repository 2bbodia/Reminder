import {Input} from "@mui/joy";
import React, {useState} from "react";
import {RecurringMessageService} from "../../../services"
import {useNavigate} from "react-router-dom";
import Button from "@mui/joy/Button";

const recurringMessageService = new RecurringMessageService();

const tg = window.Telegram.WebApp;
export default function MonthForm() {
    const navigate = useNavigate();
    const [day,setDay] = useState("")
    const [hours, setHours] = useState("");
    const [minutes, setMinutes] = useState("");
    const [message, setMessage] = useState("");

    const handleDayChange = (event) =>{
        setDay(event.target.value)
    }
    const handleHoursChange = (event) =>{
        setHours(event.target.value)
    }
    const handleMinutesChange = (event) => {
        setMinutes(event.target.value);
    };

    const handleMessageChange = (event) => {
        setMessage(event.target.value)
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        const userId =  tg.initDataUnsafe.user?.id;
        recurringMessageService.createMonthlyMessage(message,userId,day,hours,minutes)
            .then(() => {
                navigate(-1);
            })

    };

    return (
        <div>
            <form className={"createForm"} onSubmit={handleSubmit}>
            <Input
                placeholder="День"
                variant={"solid"}
                size={"lg"}
                color={"warning"}
                value={day}
                onChange={handleDayChange}
            />
            <Input
                placeholder="Год."
                variant={"solid"}
                size={"lg"}
                color={"warning"}
                value={hours}
                onChange={handleHoursChange}
            />
            <Input
                placeholder="Хв."
                variant={"solid"}
                size={"lg"}
                color={"warning"}
                value={minutes}
                onChange={handleMinutesChange}
            />
            <Input
                placeholder="Напишіть ваше повідомлення"
                variant={"solid"}
                size={"lg"}
                color={"warning"}
                value={message}
                onChange={handleMessageChange}
            />
                <Button type="submit">Submit</Button>
            </form>
        </div>
    );
}