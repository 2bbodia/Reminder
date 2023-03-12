import { useNavigate } from "react-router-dom";

import "./Menu.css";
import { CssVarsProvider, extendTheme } from "@mui/joy/styles";
import Button from "@mui/joy/Button";
import Stack from "@mui/joy/Stack";
import { useEffect } from "react";
import ScheduleSendIcon from "@mui/icons-material/ScheduleSend";
import LoopIcon from "@mui/icons-material/Loop";

const theme = extendTheme({
  components: {
    JoyButton: {
      defaultProps: {},
      styleOverrides: {
        root: {
          width: "300px",
        },
      },
    },
  },
});

const tg = window.Telegram.WebApp;

export default function Menu() {
  const navigate = useNavigate();

  useEffect(() => {
    tg.ready();
    tg.BackButton.onClick(() => navigate(-1));
    tg.BackButton.hide();
  }, []);

  const handleScheduledMessagesClick = () => {
    navigate("/delayedMessages");
  };

  const handleRecurringMessagesClick = () => {
    navigate("/recurringMessages");
  };

  return (
    <div style={{ paddingTop: "10px" }}>
      <CssVarsProvider theme={theme}>
        <Stack
          spacing={1}
          justifyContent="center"
          alignItems="center"
          sx={{ width: "100%" }}
        >
          <Button
            color="success"
            variant="soft"
            onClick={handleScheduledMessagesClick}
            size="lg"
            startDecorator={<ScheduleSendIcon />}
          >
            Одноразові нагадування
          </Button>
          <Button
            color="success"
            variant="soft"
            size="lg"
            startDecorator={<LoopIcon />}
            onClick={handleRecurringMessagesClick}
          >
            Багаторазові нагадування
          </Button>
        </Stack>
      </CssVarsProvider>
    </div>
  );
}
