import Axios from 'axios';
import React from 'react';
import { withRouter } from 'react-router';
import { openSuccessNotification, openErrorNotification } from '../helpers/notification-helper';
import { Card, Spin } from 'antd';

class EventDetailsComponent extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            event: undefined
        };

        this.loadEvent();
    }

    loadEvent = () => {
        const { id } = this.props.match.params;

        Axios.get(`${window.location.origin}/event/${id}`)
            .then(({ data }) =>
            {
                if (data) {
                    this.setState({
                        event: data
                      });
                    openSuccessNotification("Event loaded successfully");
                } else {
                    openErrorNotification("Failed to load event");
                }
            })
            .catch(error => {
                console.error(error);
                openErrorNotification("Failed to load event");
            });
    }

    render () {
        const { event } = this.state;
        
        if (!event)
            return (
                <div className="center-loading">
                    <Spin />
                </div>
            );

        return (
            <Card title={event.title}>
                <p><strong>Id: </strong> {event.id}</p>
                <p><strong>Title: </strong> {event.title}</p>
                <p><strong>Link: </strong><a href={event.link}> {event.link}</a></p>
                <p><strong>DateTime: </strong> {event.closed}</p>
            </Card>
        )
    }
}

export const EventDetailsComponentWithRouter = withRouter(EventDetailsComponent);