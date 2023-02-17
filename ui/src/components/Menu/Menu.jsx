import {useNavigate} from "react-router-dom";
import "./Menu.css";
import {CssVarsProvider, extendTheme} from '@mui/joy/styles';
import Button from '@mui/joy/Button';
import Stack from '@mui/joy/Stack';
import {useEffect} from "react";


const theme = extendTheme({
    components: {
        JoyButton: {
            defaultProps: {},
            styleOverrides: {
                root: {
                    width: "200px"
                },
            },
        },
    },
});

const tg = window.Telegram.WebApp;

export default function Menu() {
    const navigate = useNavigate();

    useEffect(() => {
        tg.ready()
        tg.BackButton.onClick(() => navigate(-1))
        tg.BackButton.hide()
        
    }, [])

    const handleRemindersClick = () => {
        navigate('/delayedMessages')
        
    }

    return (
        <div style={{paddingTop: "10px"}}>
            <CssVarsProvider theme={theme}>
                <Stack spacing={1} justifyContent="center" alignItems="center" sx={{width: '100%'}}>

                    <Button color="success" variant="soft" onClick={handleRemindersClick} size="lg">Мої
                        нагадування</Button>
                    <Button color="success" variant="soft" size="lg">Something else</Button>
                </Stack>
            </CssVarsProvider>
        </div>
    )
}