import "./index.css";
import * as signalR from "@aspnet/signalr";
import * as React from "react";
import * as ReactDOM from "react-dom";
import * as uuidv4  from "uuid/v4";

import {Board as Board} from "./Board";


(async () => {
    try {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/hub")
            .build();

        const getGameState = async (gameId) => {
            await connection.send("getGameState", gameId)
        };
        
        const makeMove = async (columnNumber) => {
            const gameId = window.location.pathname.substr(1);
            await connection.send("makeMove", window.location.pathname.substr(1), "fooo", columnNumber);
            await getGameState(gameId);
        };

        connection.on("GetGameStateResponse", (gameId: string, game: any) => {
            history.pushState(null, null, gameId);
            ReactDOM.render(
                <Board game={game} makeMove={makeMove} />,
                document.getElementById("react-root")
            );
        });

        await connection.start();
        let gameId =  window.location.pathname.substr(1);
        if (gameId === "") {
            gameId = uuidv4();
            history.pushState(null, null, gameId);
        }
        await getGameState(gameId)

    } catch (e) {
        document.write(e)
    }
})();