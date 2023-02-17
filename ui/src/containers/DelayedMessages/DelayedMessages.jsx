import {Fragment, useEffect, useState} from "react";
import Button from "@mui/joy/Button";
import {CssVarsProvider} from "@mui/joy/styles";
import "./DelayedMessages.css";
import {DelayedMessageService} from "../../services";
import {Pagination, PaginationItem} from "@mui/material";
import {DelayedMessageList} from "../../components"
import {useNavigate} from "react-router-dom";

const delayedMessageService = new DelayedMessageService();

const tg = window.Telegram.WebApp;
export default function DelayedMessages() {
    const navigate = useNavigate();
    

    const [delayedMessages, setDelayedMessages] = useState([]);
    const [paginationCount, setPaginationCount] = useState(1);
    const [page, setPage] = useState(1);
    const pageSize = 9;

    useEffect(() => {
        //getDelayedMessages();
        tg.expand()
        tg.BackButton.show()
    }, [])
    
    useEffect(() => {
        //getDelayedMessages();
    }, [page])

    const handlePageChange = (event, value) => {
        setPage(value);
    };
    
    const getDelayedMessages = () => {
        const userId  = tg.initDataUnsafe.user.id;
        // delayedMessageService
        //     .getMessagesByUserId(userId,page,pageSize,'etDesc')
        //     .then(res => res.json())
        //         .then(res => {
        //             setPage(res.pageIndex);
        //             setPaginationCount(Math.ceil(res.totalCount / pageSize))
        //             setDelayedMessages(res.data);
        //         })
        //         .catch(res => {
        //             tg.showAlert(res)
        //         })
    }
    return (
        <Fragment>
            <CssVarsProvider/>
            <div className={"createButton"}>
                <Button color="warning" size="lg"  onClick = {() => navigate("/createForm")}>
                    Створити нову подію
                </Button>
            </div>
            <DelayedMessageList delayedMessages={delayedMessages}/>
            <Pagination count={paginationCount} 
                        size="large" 
                        page={page}
                        siblingCount={0}  
                        shape="rounded"
                        onChange={handlePageChange}
                        renderItem={(item) => (
                <PaginationItem
                    {...item}
                />
            )} />
        </Fragment>
    );
    
}
