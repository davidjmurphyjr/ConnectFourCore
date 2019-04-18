import "./index.css";
import * as signalR from "@aspnet/signalr";
import * as React from "react";
import * as ReactDOM from "react-dom";
import * as uuidv4  from "uuid/v4";
import {Game} from "./Game";

const GetGameIdFromLocation: () => string = () => {
    const uuidRegEx = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/;
    const pathnameWithoutLeadingSlash = window.location.pathname.substr(1);
    const patternMatch = uuidRegEx.test(pathnameWithoutLeadingSlash);
    return patternMatch ? pathnameWithoutLeadingSlash : null;
};

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
            await connection.send("makeMove", window.location.pathname.substr(1), columnNumber);
            await getGameState(gameId);
        };

        connection.on("GameStateAnnounce", (gameId: string, game: any) => {
            const locationGameId = GetGameIdFromLocation();
            if(gameId === locationGameId) {
                const reactRootElement = document.getElementById("react-root");
                ReactDOM.render( <Game game={game} makeMove={makeMove} />, reactRootElement);
            }
        });

        await connection.start();
        let gameId =  GetGameIdFromLocation();
        if (!gameId) {
            gameId = uuidv4();
            history.pushState(null, null, gameId);
        }
        await getGameState(gameId)

    } catch (e) {
        document.write(e)
    }
})();