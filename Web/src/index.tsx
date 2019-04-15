import "./index.css";
import * as signalR from "@aspnet/signalr";
import * as React from "react";
import * as ReactDOM from "react-dom";

import {Board as Board} from "./Board";

(async () => {
    try {
        const username = new Date().getTime();
        const pathname = window.location.pathname;
        const gameId = pathname.substr(1);

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/hub")
            .build();
        
        const makeMove = async (columnNumber) => {
            await connection.send("makeMove", gameId, "fooo", columnNumber);
            connection.send("getGameState", gameId)
        };

        connection.on("gameCreated", (gameId: string, game: any) => {
            history.pushState(null, null, gameId);
            ReactDOM.render(
                <Board board={game.board} makeMove={makeMove} />,
                document.getElementById("react-root")
            );
        });

        connection.on("GetGameStateResponse", (gameId: string, game: any) => {
            history.pushState(null, null, gameId);
            ReactDOM.render(
                <Board board={game.board} makeMove={makeMove} />,
                document.getElementById("react-root")
            );
        });

        await connection.start();
        if (pathname === "/") {
            connection.send("newGame", username, "fooo")
        } else {
            connection.send("getGameState", gameId)
        }
       
    } catch (e) {
        document.write(e)
    }
})();