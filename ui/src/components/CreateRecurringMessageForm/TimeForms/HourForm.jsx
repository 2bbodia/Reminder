import React, {useState} from "react";
import {Input} from "@mui/joy";
import Button from "@mui/joy/Button";
import {RecurringMessageService} from "../../../services"
import {useNavigate} from "react-router-dom";

const recurringMessageService = new RecurringMessageService();

const tg = window.Telegram.WebApp;

export default function  HourForm() {
    const navigate = useNavigate();
    const [minutes, setMinutes] = useState("");
    const [message, setMessage] = useState("");

    const handleMinutesChange = (event) => {
        setMinutes(event.target.value);
    };

    const handleMessageChange = (event) => {
        setMessage(event.target.value)
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        const userId =  tg.initDataUnsafe.user?.id;
        recurringMessageService.createHourlyMessage(message,userId,minutes)
            .then(() => {
                navigate(-1);
            })

    };

    return (
        <div>
            <form className={"createForm"} onSubmit={handleSubmit}>
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
