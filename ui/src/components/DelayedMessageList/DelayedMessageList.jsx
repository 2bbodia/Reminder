import Delete from "@mui/icons-material/Delete";
import { CssVarsProvider } from "@mui/joy/styles";
import { IconButton } from "@mui/joy";
import "./DelayedMessageList.css";
import Table from "@mui/joy/Table";
import { Fragment } from "react";

export default function DelayedMessageList({
  delayedMessages,
  deleteButtonHandler,
}) {
    if(delayedMessages.length === 0) return (<Fragment></Fragment>)
  return (
    <div>
        <CssVarsProvider />
        <Table variant={'solid'} color={'warning'}>
          <thead>
            <tr>
              <th>Повідомлення</th>
              <th>Дата відсилання</th>
              <th>Відмінити</th>
            </tr>
          </thead>
          <tbody>
            {delayedMessages.map((message) => (
              <tr key={message.id} >
                <td>{message.text}</td>
                <td>{message.enqueueAt}</td>
                <td  className="cancelButton"> 
                  <IconButton
                    aria-label="Delete"
                    size="sm"
                    color="danger"
                   
                    onClick={() => deleteButtonHandler(message.id)}
                  >
                    <Delete />
                  </IconButton>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
    </div>
  );
}
