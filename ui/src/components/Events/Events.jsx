import "./Events.css";
import { Calendar, dayjsLocalizer } from "react-big-calendar";
import dayjs from "dayjs";
import "react-big-calendar/lib/css/react-big-calendar.css";
import { useEffect, useState, useCallback } from "react";
import Box from "@mui/joy/Box";
import Button from "@mui/joy/Button";
import Divider from "@mui/joy/Divider";
import Modal from "@mui/joy/Modal";
import ModalDialog from "@mui/joy/ModalDialog";
import Typography from "@mui/joy/Typography";
import { EventsService } from "../../services";
import { useNavigate } from "react-router-dom";
import {CssVarsProvider} from "@mui/joy/styles";

const eventService = new EventsService();

const localizer = dayjsLocalizer(dayjs);

const tg = window.Telegram.WebApp;

export default function Events() {

  useEffect(() => {
    getEvents();
    tg.MainButton.setText("Створити подію");
    tg.MainButton.onClick(onCreateClick);
    tg.MainButton.show();
    return () => {
      tg.MainButton.offClick(onCreateClick);
      tg.MainButton.hide();
    };
  }, []);

  const [selectedEvent, setSelectedEvent] = useState({});
  const [events, setEvents] = useState([]);
  const [open, setOpen] = useState(false);
  const [date, setDate] = useState(new Date());
  const navigate = useNavigate();

  const onSelectEvent = (calEvent) => {
    setSelectedEvent(calEvent);
    setOpen(true);
  };

    const eventPropGetter = useCallback(
      (event, start, end, isSelected) => ({
        ...(event.resource.importance === "Low" && {
          style: {
            backgroundColor: 'var(--joy-palette-success-solidBg)'
            },
            }),
        ...(event.resource.importance === "Medium" && {
          style: {
            backgroundColor: 'var(--joy-palette-warning-solidBg)'
            },
          }),
        ...(event.resource.importance === "High" && {
          style: {
            backgroundColor: 'var(--joy-palette-danger-solidBg)'
            },
          })
      }),
      []
    )

  const handleDeleteClick = () => {
    eventService.deleteEvent(selectedEvent.id).then(() => getEvents());
    setSelectedEvent({});
    setOpen(false);
  };
  const onNavigate = useCallback((newDate) => setDate(newDate), [setDate]);

  const getEvents = () => {
    eventService
      .getEventsByUserId(tg.initDataUnsafe.user.id, date.toISOString().slice(0, 10))
      .then(res => res.json())
      .then((data) => {
        const events = data.map((event) => {
          const startDateUtc = event.startDate;
          const startDate = new Date(Date.UTC(
            startDateUtc.slice(0, 4),
            startDateUtc.slice(5, 7) - 1,
            startDateUtc.slice(8, 10),
            startDateUtc.slice(11, 13),
            startDateUtc.slice(14, 16),
            startDateUtc.slice(17, 19)
          ));

          const endDateUtc = event.endDate;
          const endDate = new Date(Date.UTC(
            endDateUtc.slice(0, 4),
            endDateUtc.slice(5, 7) - 1,
            endDateUtc.slice(8, 10),
            endDateUtc.slice(11, 13),
            endDateUtc.slice(14, 16),
            endDateUtc.slice(17, 19)
          ));
          return {
            id: event.id,
            title: event.title,
            start: startDate,
            end: endDate,
            resource: {
              description: event.description,
              importance: event.importance
            },
          };
        });
        setEvents(events);
      });
  };

  const onCreateClick = () => {
    navigate("/createEvent");
  };

  return (
    <div className="eventsContainer" style={{ position: "relative" }}>
            <CssVarsProvider/>
      <Calendar
        localizer={localizer}
        events={events}
        view="day"
        views={["day"]}
        style={{ width: "370px" }}
        onSelectEvent={onSelectEvent}
        onNavigate={onNavigate}
        date={date}
        eventPropGetter={eventPropGetter}
      />

      <Modal open={open} onClose={() => setOpen(false)}>
        <ModalDialog 
        variant="outlined"
          role="alertdialog"
          aria-labelledby="alert-dialog-modal-title"
          aria-describedby="alert-dialog-modal-description">
          <Typography id="alert-dialog-modal-title" component="h2">
            {selectedEvent.title}
          </Typography>
          <Divider />
          <Typography
            id="alert-dialog-modal-description"
            textColor="text.tertiary"
          >
            {selectedEvent.resource?.description}
          </Typography>
          <Box
            sx={{ display: "flex", gap: 1, justifyContent: "flex-end", pt: 2 }}
          >
            <Button
              variant="plain"
              color="neutral"
              onClick={() => setOpen(false)}
            >
              Ok
            </Button>
            <Button variant="solid" color="danger" onClick={handleDeleteClick}>
              Delete
            </Button>
          </Box>
        </ModalDialog>
      </Modal>

     
    </div>
  );
}
