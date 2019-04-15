import "./index.css";
import * as signalR from "@aspnet/signalr";
import * as React from "react";
import * as ReactDOM from "react-dom";

import {Board as Board} from "./Board";

var re = /\[([a-f0-9]{8}(?:-[a-f0-9]{4}){3}-[a-f0-9]{12})\]/i;
function extractGuid(value) {    

    // the RegEx will match the first occurrence of the pattern
    var match = re.exec(value);

    // result is an array containing:
    // [0] the entire string that was matched by our RegEx
    // [1] the first (only) group within our match, specified by the
    // () within our pattern, which contains the GUID value

    return match ? match[1] : null;
}

(async () => {
    try {
        const username = new Date().getTime();

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/hub")
            .build();

        connection.on("gameCreated", (gamId: string, game: any) => {
            ReactDOM.render(
                <Board board={game.board} />,
                document.getElementById("react-root")
            );
        });

        connection.on("gameCreated", (username: string, gameId: string) => {
            history.pushState(null, null, gameId);
        });

        await connection.start();
        connection.send("newGame", username, "fooo")
       
    } catch (e) {
        document.write(e)
    }
})();