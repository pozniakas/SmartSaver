import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'
import { FetchTransactions } from './components/FetchTransactions';

export default class App extends Component {
    static displayName = App.name;
    static apiUrl = "https://localhost:44322/";

    static async fetchApi(url) {
        return await fetch(this.apiUrl + url);
    }

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} /> 
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
            <Route path='/fetch-transactions' component={FetchTransactions} />
      </Layout>
    );
  }
}
