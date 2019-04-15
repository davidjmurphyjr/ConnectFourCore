import "./index.css";
import * as signalR from "@aspnet/signalr";
import * as React from "react";
import * as ReactDOM from "react-dom";

import {Board as Board} from "./Board";


(async () => {
    try {

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/hub")
            .build();
        
        const makeMove = async (columnNumber) => {
            const gameId = window.location.pathname.substr(1);
            await connection.send("makeMove", window.location.pathname.substr(1), "fooo", columnNumber);
            await connection.send("getGameState", gameId)
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
        const gameId =  window.location.pathname.substr(1);
        if (gameId === "") {
            await connection.send("newGame")
        } else {
            await connection.send("getGameState", gameId)
        }
       
    } catch (e) {
        document.write(e)
    }
})();