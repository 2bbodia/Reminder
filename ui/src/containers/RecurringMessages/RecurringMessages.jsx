import { Fragment, useEffect, useState } from "react";
import Button from "@mui/joy/Button";
import { CssVarsProvider } from "@mui/joy/styles";
import { Pagination, PaginationItem } from "@mui/material";
import { useNavigate } from "react-router-dom";
import "./RecurringMessages.css";
import { RecurringMessageService } from "../../services";
import { RecurringMessageList } from "../../components";
import {calculateMessagesCountForPage} from "../../helpers"

const recurringMessageService = new RecurringMessageService();

const tg = window.Telegram.WebApp;

export default function RecurringMessages() {
  const navigate = useNavigate();

  const [pageOptions, setPageOptions] = useState(()=>{
    const initialPageSize = calculateMessagesCountForPage(tg.viewportStableHeight);
    return {
      page: 1,
      pageSize: initialPageSize,
      paginationCount:0,
      totalCount: 0,
    }
  }
  );
  const [recurringMessages, setRecurringMessages] = useState([]);

  useEffect(() => {
    tg.onEvent("viewportChanged", handleViewportChange);
    tg.BackButton.show();
    getRecurringMessages();

    return () => {
      tg.offEvent("viewportChanged", handleViewportChange);
    };
  }, []);

  useEffect(() => {
    getRecurringMessages();
  }, [pageOptions.page, pageOptions.pageSize]);


  const handleViewportChange = (e) => {
    const newPageSize = calculateMessagesCountForPage(tg.viewportStableHeight);
    setPageOptions({
      ...pageOptions,
      pageSize: newPageSize,
    });
  };

  const handlePageChange = (event, value) => {
    setPageOptions({
      ...pageOptions,
      page: value,
    });
  };
  
  const getRecurringMessages = () => {
    const userId = tg.initDataUnsafe.user.id;
  recurringMessageService
  .getMessagesByUserId(userId,pageOptions.page,pageOptions.pageSize,"etDesc")
      .then((res) => res.json())
      .then((res) => {
        setPageOptions(
          {
            page: res.pageIndex, 
            totalCount: res.totalCount,
            paginationCount: Math.ceil(res.totalCount / res.pageSize),
            pageSize: res.pageSize
          });
        let messages = res.data;
        messages = messages.map((message) => {
          const date = new Date(message.nextExecution);
          message.nextExecution = date.toLocaleString();
          return message;
        });
        setRecurringMessages(messages);
      })
      .catch((res) => {
        tg.showAlert(res);
      });
  };

  const handleDeleteButtonClick =( id ) => {
    recurringMessageService.deleteMessage(id).then(()=>{
      getRecurringMessages();
    })
    .catch((err) => {
      tg.showAlert(res);
    });
    }

  return (
    <Fragment>
      <CssVarsProvider />
      <div className={"createButton"}>
        <Button
          color="warning"
          size="lg"
          onClick={() => navigate("/createRecurringMessage")}
        >
          Створити нову подію
        </Button>
      </div>
      <div className="messageList">
      <RecurringMessageList
        recurringMessages={recurringMessages}
        deleteButtonHandler={handleDeleteButtonClick}
      />
      </div>
      {pageOptions.totalCount > pageOptions.pageSize && (
        <Pagination
          count={pageOptions.paginationCount}
          size="large"
          page={pageOptions.page}
          siblingCount={0}
          shape="rounded"
          onChange={handlePageChange}
          renderItem={(item) => <PaginationItem {...item} />}
        />
      )}
      
    </Fragment>
  );
}
