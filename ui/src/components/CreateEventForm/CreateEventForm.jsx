import { Input } from "@mui/joy";
import Button from "@mui/joy/Button";
import { CssVarsProvider } from "@mui/joy/styles";
import TextField from "@mui/material/TextField";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import React from "react";
import "./CreateEventForm.css";
import { EventsService } from "../../services";
import { useNavigate } from "react-router-dom";
import Radio from "@mui/joy/Radio";
import FormControl from "@mui/joy/FormControl";
import FormLabel from "@mui/joy/FormLabel";
import RadioGroup from "@mui/joy/RadioGroup";
import Modal from "@mui/joy/Modal";
import ModalDialog from "@mui/joy/ModalDialog";
import Typography from "@mui/joy/Typography";
import Box from "@mui/joy/Box";
import Divider from "@mui/joy/Divider";

const eventsService = new EventsService();

const tg = window.Telegram.WebApp;

export default function CreateEventForm() {
  const [open, setOpen] = React.useState(false);
  const [data, setData] = React.useState({
    title: "",
    description: "",
    startDate: dayjs(),
    endDate: dayjs(),
  });

  React.useEffect(() => {
    tg.BackButton.onClick(returnButtonHandleClick);
    tg.BackButton.show();
    return () => {
      tg.BackButton.offClick(returnButtonHandleClick);
      tg.BackButton.hide();
    };
  }, []);

  const returnButtonHandleClick = () => {
    navigate(-1);
  };

  const [selectedImportance, setSelectedImportance] = React.useState("Low");

  const handleImportanceChange = (event) => {
    setSelectedImportance(event.target.value);
  };

  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();
    eventsService
      .isExist(tg.initDataUnsafe.user.id, data.startDate, data.endDate)
      .then((res) => {
        console.log(res);
        if (res.status === 204) {
          createEvent();
          return;
        }
        if (res.status === 200) {
          setOpen(true);
          return;
        }
      });
  };

  const createEvent = () => {
    const remindAt = new Date(data.startDate);
    remindAt.setMinutes(remindAt.getMinutes() - 10);
    eventsService
      .createEvent(
        tg.initDataUnsafe.user.id,
        data.title,
        data.description,
        data.startDate,
        data.endDate,
        remindAt,
        selectedImportance
      )
      .then(() => {
        navigate(-1);
      });
  };

  const onStartDateChange = (newValue) => {
    setData((prev) => ({
      ...prev,
      startDate: dayjs(newValue),
      endDate: dayjs(newValue),
    }));
  };

  const onEndDateChange = (newValue) => {
    setData((prev) => ({
      ...prev,
      endDate: dayjs(newValue),
    }));
  };

  const onTitleChange = (event) => {
    setData((prev) => ({
      ...prev,
      title: event.target.value,
    }));
  };

  const onDescriptionChange = (event) => {
    setData((prev) => ({
      ...prev,
      description: event.target.value,
    }));
  };
  return (
    <div>
      <CssVarsProvider />
      <form className="createForm" onSubmit={handleSubmit}>
        <Input
          className="formItem"
          placeholder="Title"
          onChange={onTitleChange}
        />
        <Input
          className="formItem"
          placeholder="Description"
          onChange={onDescriptionChange}
        />
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <div className="formItem">
            <DateTimePicker
              className="dateTimePicker"
              renderInput={(props) => <TextField {...props} />}
              value={data.startDate}
              onChange={onStartDateChange}
              ampm={false}
              disablePast={true}
            />
          </div>
          <div className="formItem">
            <DateTimePicker
              className="dateTimePicker"
              renderInput={(props) => <TextField {...props} />}
              value={data.endDate}
              onChange={onEndDateChange}
              ampm={false}
              disablePast={true}
              minDateTime={data.startDate}
            />
          </div>
        </LocalizationProvider>
        <FormControl className="formItem">
          <FormLabel>Важливість події</FormLabel>
          <RadioGroup
            orientation="horizontal"
            name="controlled-radio-buttons-group"
            value={selectedImportance}
            onChange={handleImportanceChange}
            sx={{ my: 1 }}
          >
            <Radio
              value="Low"
              name="radio-buttons"
              variant="solid"
              color="success"
              label="Низька"
            />
            <Radio
              value="Medium"
              name="radio-buttons"
              variant="solid"
              color="warning"
              label="Середня"
            />
            <Radio
              value="High"
              name="radio-buttons"
              color="danger"
              variant="solid"
              label="Висока"
            />
          </RadioGroup>
        </FormControl>
        <Button className="formItem" type="submit">
          Submit
        </Button>
      </form>
      <Modal open={open} onClose={() => setOpen(false)}>
        <ModalDialog
          variant="outlined"
          role="alertdialog"
          aria-labelledby="alert-dialog-modal-title"
          aria-describedby="alert-dialog-modal-description"
        >
          <Typography id="alert-dialog-modal-title" component="h2">
            {"Попередження"}
          </Typography>
          <Divider />
          <Typography
            id="alert-dialog-modal-description"
            textColor="text.tertiary"
          >
            {"Уже існує подія на цей час"}
          </Typography>
          <Box
            sx={{ display: "flex", gap: 1, justifyContent: "flex-end", pt: 2 }}
          >
            <Button
              variant="plain"
              color="neutral"
              onClick={() => setOpen(false)}
            >
              Відмінити
            </Button>
            <Button variant="solid" color="success" onClick={createEvent}>
              Створити
            </Button>
          </Box>
        </ModalDialog>
      </Modal>
    </div>
  );
}
