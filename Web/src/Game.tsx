import "./Game.css";
import * as React from "react";
import {Column} from "./Column";

export interface GameProps { game: any; makeMove: any }

export class Game extends React.Component<GameProps, {}> {
    render() {
        const {columns, tokenToDrop, numberOfMovesMade, winner} = this.props.game;
        let winnerText = "None";
        if(winner === 0) {
            winnerText = "Red"
        }
        if(winner === 1) {
            winnerText = "Yellow"
        }
        return (
            <div className="game" >
                <h1>Connect 4</h1>
                <div className="scoreboard">
                    <div className="metric">
                        Moves: {numberOfMovesMade}
                    </div>
                    <div className="action">
                        <a href="/">New Game</a>
                    </div>
                    <div className="metric">
                        Winner: {winnerText}
                    </div>
                </div>
                <div className="board">
                    {columns.map((column, columnNumber) => (
                        <Column tokenToDrop={tokenToDrop} column={column} onClick={async () => await this.props.makeMove(columnNumber)} />
                    ))}
                </div>
            </div>
        );
    }
}





