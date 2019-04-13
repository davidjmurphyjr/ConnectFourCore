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
                    return (
                        <div className="column">
                            {column.tokens.map(token => {
                                return (
                                    <div className="space">
                                        {token}
                                    </div>
                                );
                            })}
                        </div>
                    );
                })}
            </div>
        );
    }
}





