import "./Board.css";
import * as React from "react";

export interface BoardProps {
    game: any;
    makeMove: any
}

export class Board extends React.Component<BoardProps, {}> {
    appendTokenColor = (className, token) => {
        if (token === 0) {
            return className += " yellow"
        }
        if (token === 1) {
            return className += " red"
        }
        return className;
    };

    render() {
        const {columns, tokenToDrop} = this.props.game;
        return (
            <div className="board">
                {columns.map((column, columnNumber) => {
                    const tokenCircle = <circle cx="50" cy="50" r="45"/>;
                    return (
                        <div className="column" onClick={async () => await this.props.makeMove(columnNumber)}>
                            <svg className={this.appendTokenColor("token-to-drop", tokenToDrop)} viewBox="0 0 100 100"
                                 version="1.1"
                                 xmlns="http://www.w3.org/2000/svg">
                                {tokenCircle}
                            </svg>
                            {column.reverse().map(space => {
                                return (
                                    <svg className={this.appendTokenColor("space", space)} viewBox="0 0 100 100"
                                         version="1.1"
                                         xmlns="http://www.w3.org/2000/svg">
                                        <rect x="0" y="0" width="100" height="100"/>
                                        {tokenCircle}
                                    </svg>
                                );
                            })}
                        </div>
                    );
                })}
            </div>
        );
    }
}





