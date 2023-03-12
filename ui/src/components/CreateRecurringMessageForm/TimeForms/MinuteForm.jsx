import {Input} from "@mui/joy";
import React, {useState} from "react";
import {RecurringMessageService} from "../../../services"
import {useNavigate} from "react-router-dom";
import Button from "@mui/joy/Button";

const recurringMessageService = new RecurringMessageService();

const tg = window.Telegram.WebApp;

export default  function MinuteForm() {
    const navigate = useNavigate();

    const [message, setMessage] = useState("");

    const handleMessageChange = (event) => {
        setMessage(event.target.value)
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        const userId =  tg.initDataUnsafe.user?.id;
        recurringMessageService.createMinutelyMessage(message,userId)
            .then(() => {
                navigate(-1);
            })

    };
    return (
        <div>
            <form className={"createForm"} onSubmit={handleSubmit}>
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
