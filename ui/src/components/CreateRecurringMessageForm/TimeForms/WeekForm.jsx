import {Input} from "@mui/joy";
import React, {useState} from "react";
import {RecurringMessageService} from "../../../services"
import {useNavigate} from "react-router-dom";
import Button from "@mui/joy/Button";
import Select from "@mui/joy/Select";
import Option from "@mui/joy/Option";

const recurringMessageService = new RecurringMessageService();

const daysOfWeek = [
    {value:0, label:"Неділя"},
    {value:1, label:"Понеділок"},
    {value:2, label:"Вівторок"},
    {value:3, label:"Середа"},
    {value:4, label:"Четвер"},
    {value:5, label:"П'ятниця"},
    {value:6, label:"Субота"}
]

const tg = window.Telegram.WebApp;
export default function WeekForm() {
    const navigate = useNavigate();
    const [dayOfWeek,setDayOfWeek] = useState(null);
    const [hours, setHours] = useState("");
    const [minutes, setMinutes] = useState("");
    const [message, setMessage] = useState("");

    const handleDayOfWeekChange = (event,newOption)=>{
        setDayOfWeek(newOption)
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
        recurringMessageService.createWeeklyMessage(message,userId,dayOfWeek,hours,minutes)
            .then(() => {
                navigate(-1);
            })

    };
    return (
        <div>
            <form className={"createForm"} onSubmit={handleSubmit}>
                <Select
                    variant="solid"
                    color="warning"
                    size="lg"
                    onChange={handleDayOfWeekChange}
                >
                    {
                        daysOfWeek.map((option) => (
                        <Option key={option.value} value={option.value}>
                            {option.label}
                        </Option>
                    ))}
                </Select>
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