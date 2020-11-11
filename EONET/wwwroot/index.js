import React from 'react';
import ReactDOM from 'react-dom';
import {
    BrowserRouter as Router,
    Switch,
    Route
  } from "react-router-dom";
import 'antd/dist/antd.css';
import './css/site.css';
import { EventListComponent } from './components/event-list-component';
import { EventDetailsComponentWithRouter } from './components/event-details-component';

global.renderPage = function () {
    const mainNode = document.getElementById("react-content");

    ReactDOM.render(
    <Router>
        <Switch>
          <Route exact path="/">
            <EventListComponent />
          </Route>
          <Route path="/events/:id" component={EventDetailsComponentWithRouter} />
        </Switch>
    </Router>, mainNode
    );
};