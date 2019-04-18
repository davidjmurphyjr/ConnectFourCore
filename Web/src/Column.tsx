import "./Column.css";
import * as React from "react";
import {Space} from "./Space";
import classNames  from "classnames";
import Token from "./Token";

export interface ColumnProps { tokenToDrop: number, onClick: any, column: any }

export class Column extends React.Component<ColumnProps, {}> {
    render() {
        const {tokenToDrop, column} = this.props;
        return (
            <div className="column" onClick={this.props.onClick}>
                <Space className={classNames("drop-space", {red: tokenToDrop == 0, yellow: tokenToDrop == 1})}/>
                {column.reverse().map(token =>
                    <Space className={classNames("board-space", {red: token == 0, yellow: token == 1})}/>
                )}
            </div>
        );
    }
}





