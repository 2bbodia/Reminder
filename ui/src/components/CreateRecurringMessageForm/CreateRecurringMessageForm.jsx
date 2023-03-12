import {CssVarsProvider} from "@mui/joy/styles";
import React, {useState} from "react";
import "./CreateRecurringMessageForm.css";
import {useNavigate} from "react-router-dom";
import Select from "@mui/joy/Select";
import Option from "@mui/joy/Option";
import {MinuteForm, HourForm, DayForm, WeekForm, MonthForm, YearForm} from "./TimeForms";


const tg = window.Telegram.WebApp;


const options = [
    {value: "minutely", label: "Minutely", form: MinuteForm},
    {value: "hourly", label: "Hourly", form: HourForm},
    {value: "daily", label: "Daily", form: DayForm},
    {value: "weekly", label: "Weekly", form: WeekForm},
    {value: "monthly", label: "Monthly", form: MonthForm},
    {value: "yearly", label: "Yearly", form: YearForm},
];

export default function CreateRecurringMessageForm() {
    const navigate = useNavigate();

    const [selectedOption, setSelectedOption] = useState(options[1].value);

    const handleOptionChange = (event, newValue) => {
        setSelectedOption(newValue);
    };

    const InputComponent = options.find(
        (option) => option.value === selectedOption
    ).form;


    return (
        <div>
            <CssVarsProvider/>

            <Select
                value={selectedOption}
                onChange={handleOptionChange}
                variant="solid"
                color="warning"
                size="lg"
            >
                {options.map((option) => (
                    <Option key={option.value} value={option.value}>
                        {option.label}
                    </Option>
                ))}
            </Select>
            <InputComponent/>

        </div>
    );
}
