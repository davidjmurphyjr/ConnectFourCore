import "./Board.css";
import * as React from "react";

export interface BoardProps { board: any; }

// 'HelloProps' describes the shape of props.
// State is never set so we use the '{}' type.
export class Board extends React.Component<BoardProps, {}> {
    render() {
        return (
            <div className="board">
                {this.props.board.columns.map(column => {
                    const tokenCircle = <circle cx="50" cy="50" r="45"/>;
                    return (
                        <div className="column">
                            <svg className="staging" viewBox="0 0 100 100" version="1.1"
                                 xmlns="http://www.w3.org/2000/svg">
                                {tokenCircle}
                            </svg>
                            {column.tokens.map(token => {
                                return (
                                    <svg className="space" viewBox="0 0 100 100" version="1.1"
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





