import "./Space.css";
import * as React from "react";
import classNames  from "classnames";

export interface SpaceProps { className: string }

export class Space extends React.Component<SpaceProps, {}> {
    render() {
        const { className } = this.props;
        return (
            <svg className={classNames("space", className)} viewBox="0 0 100 100" version="1.1" xmlns="http://www.w3.org/2000/svg">
                <rect x="0" y="0" width="100" height="100"/>
                <circle cx="50" cy="50" r="45"/>;
            </svg>
        );
    }
}