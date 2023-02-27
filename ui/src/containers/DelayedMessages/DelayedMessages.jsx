import { Fragment, useEffect, useState ,useCallback} from "react";
import Button from "@mui/joy/Button";
import { CssVarsProvider } from "@mui/joy/styles";
import "./DelayedMessages.css";
import { DelayedMessageService } from "../../services";
import { Pagination, PaginationItem } from "@mui/material";
import { DelayedMessageList } from "../../components";
import { useNavigate } from "react-router-dom";

const delayedMessageService = new DelayedMessageService();

const tg = window.Telegram.WebApp;
export default function DelayedMessages() {
  const navigate = useNavigate();

  const [delayedMessages, setDelayedMessages] = useState([]);
  const [paginationCount, setPaginationCount] = useState(0);
  const [page, setPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(0);

  useEffect(() => {
    tg.expand();
    tg.BackButton.show();
    const initialPageSize = calculateMessagesCountForPage(tg.viewportStableHeight);
    setPageSize(initialPageSize);
  }, []);

  useEffect(() => {
    getDelayedMessages();
  },[pageSize]);

  useEffect(() => {
    getDelayedMessages();
  }, [page]);

  const handlePageChange = (event, value) => {
    setPage(value);
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


  const getDelayedMessages = useCallback(() => {
    if(!pageSize) return;
    const userId = tg.initDataUnsafe.user.id;
    console.log('userId:', userId);
  console.log('page:', page);
  console.log('pageSize:', pageSize);
    delayedMessageService
      .getMessagesByUserId(userId, page, pageSize, "etDesc")
      .then((res) => res.json())
      .then((res) => {
        console.log('res:', res);
        setPage(res.pageIndex);
        setTotalCount(res.totalCount);
        setPaginationCount(Math.ceil(res.totalCount / pageSize));
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
  }, [page, pageSize]);

  const calculateMessagesCountForPage = (number) => {
    const ranges = [
      [300, 400, 5],
      [400, 500, 6],
      [500, 600, 7],
      [600, 700, 8],
      [700, 800, 9],
      [800, 900, 10],
    ];

    for (let i = 0; i < ranges.length; i++) {
      const [start, end, coefficient] = ranges[i];
      if (number >= start && number < end) {
        return coefficient;
      }
    }
    return null; // якщо число не входить в жоден проміжок
  };

  return (
    <Fragment>
      <CssVarsProvider />
      <div className={"createButton"}>
        <Button
          color="warning"
          size="lg"
          onClick={() => navigate("/createForm")}
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
      
      {totalCount > pageSize && (
        <Pagination
          count={paginationCount}
          size="large"
          page={page}
          siblingCount={0}
          shape="rounded"
          onChange={handlePageChange}
          renderItem={(item) => <PaginationItem {...item} />}
        />
      )}
    </Fragment>
  );
}
