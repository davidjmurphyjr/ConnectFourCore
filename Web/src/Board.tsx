import * as React from "react";

export interface BoardProps { board: any; }

// 'HelloProps' describes the shape of props.
// State is never set so we use the '{}' type.
export class Board extends React.Component<BoardProps, {}> {
    render() {
        return (
            this.props.board.rows.map(row => {
                return (
                    <div>
                        {row.map(space => {
                            return <div>space: {space}</div>;
                        })}
                    </div>
                );
            })
        );
    }
}





