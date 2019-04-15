import "./Board.css";
import * as React from "react";

export interface BoardProps { board: any; makeMove: any }

export class Board extends React.Component<BoardProps, {}> {
    
    onColumnClick = async (columnNumber) => {
        await this.props.makeMove()
    };
    
    render() {
        return (
            <div className="board">
                {this.props.board.columns.map( (column, columnNumber) => {
                    const tokenCircle = <circle cx="50" cy="50" r="45"/>;
                    return (
                        <div className="column" onClick={async () => await this.props.makeMove(columnNumber)}>
                            <svg className="staging" viewBox="0 0 100 100" version="1.1"
                                 xmlns="http://www.w3.org/2000/svg">
                                {tokenCircle}
                            </svg>
                            {column.tokens.map(token => {
                                let className = "space";
                                if(token === 0) {
                                    className = className + " yellow"
                                }
                                if(token === 1) {
                                    className = className + " red"
                                }
                                return (
                                    <svg className={className} viewBox="0 0 100 100" version="1.1"
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





