import { Fragment, useEffect, useState } from "react";
import Button from "@mui/joy/Button";
import { CssVarsProvider } from "@mui/joy/styles";
import "./DelayedMessages.css";
import { DelayedMessageService } from "../../services";
import { Pagination, PaginationItem } from "@mui/material";
import { DelayedMessageList } from "../../components";
import { useNavigate } from "react-router-dom";
import {calculateMessagesCountForPage} from "../../helpers"

const delayedMessageService = new DelayedMessageService();


const tg = window.Telegram.WebApp;
export default function DelayedMessages() {
  const navigate = useNavigate();

  const [delayedMessages, setDelayedMessages] = useState([]);
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

  useEffect(() => {
    tg.onEvent("viewportChanged", handleViewportChange);
    tg.BackButton.show();
    getDelayedMessages();

    return () => {
      tg.offEvent("viewportChanged", handleViewportChange);
    };
  }, []);

useEffect(() => {
    getDelayedMessages();
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

  const handleDeleteButtonClick = (id) => {
    delayedMessageService
      .deleteMessage(id)
      .then((res) => {
        getDelayedMessages();
      })
      .catch((err) => {
        tg.showAlert(res);
      });
  };


  const getDelayedMessages = () => {
    const userId = tg.initDataUnsafe.user.id;
    delayedMessageService
      .getMessagesByUserId(userId, pageOptions.page, pageOptions.pageSize, "etDesc")
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
          const date = new Date(message.enqueueAt);
          message.enqueueAt = date.toLocaleString();
          return message;
        });
        setDelayedMessages(messages);
      })
      .catch((res) => {
        tg.showAlert(res);
      });
  };

 
  return (
    <Fragment>
      <CssVarsProvider />
      <div className={"createButton"}>
        <Button
          color="warning"
          size="lg"
          onClick={() => navigate("/createDelayedMessage")}
        >
          Створити нову подію
        </Button>
      </div>
      <div className="messageList">
      <DelayedMessageList
        delayedMessages={delayedMessages}
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
