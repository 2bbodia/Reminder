import {Fragment, useEffect, useState} from "react";
import Button from "@mui/joy/Button";
import {CssVarsProvider} from "@mui/joy/styles";
import "./DelayedMessages.css";
import {DelayedMessageService} from "../../services";
import {Pagination, PaginationItem} from "@mui/material";
import {DelayedMessageList} from "../../components"

const delayedMessageService = new DelayedMessageService();

const tg = window.Telegram.WebApp;
export default function DelayedMessages() {

    const [delayedMessages, setDelayedMessages] = useState([]);
    const [paginationCount, setPaginationCount] = useState(1);
    const [page, setPage] = useState(1);
    const pageSize = 9;
    
    
    
    useEffect(() => {
        getDelayedMessages();
        setPaginationCount(Math.ceil(delayedMessages.length / pageSize))
        tg.expand()
        tg.BackButton.show()
    })

    const handlePageChange = (event, value) => {
        setPage(value);
        getDelayedMessages();
    };
    
    const getDelayedMessages = () => {
        const userId  = tg.initDataUnsafe.user.id
        delayedMessageService.getMessagesByUserId(userId,page,pageSize,'etDesc').then(res => res.json()).then(
            res => {
                setDelayedMessages(res)
            }
        ).catch(
            res => {
                tg.showAlert(res)
            }
        )
    }
    return (
        <Fragment>
            <CssVarsProvider/>
            <div className={"createButton"}>
                <Button color="warning" size="lg"   >
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
