import React, { Component } from 'react';
import App from '../App';
import { Transaction } from './models/Transaction';
import { FetchCategories } from './FetchCategories';

type State = {
    transactions: string;
    loading: boolean;
};

export class FetchTransactions extends Component {
    static displayName = FetchTransactions.name;
    

    constructor(props: {}) {
        
    super(props);
      this.state = { transactions: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

    static renderTransactionsTable(transaction: Transaction[]) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Time</th>
            <th>Amount</th>
            <th>Details</th>
            <th>Category</th>
          </tr>
        </thead>
        <tbody>
          {transaction.map(transacton =>
              <tr key={transacton.id}>
                  <td>{transacton.time}</td>
                  <td>{transacton.amount}</td>
                  <td>{transacton.details}</td>
                  <td>{transacton.categoryid}</td>
              </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchTransactions.renderTransactionsTable(this.state.transactions);

    return (
      <div>
        <h1 id="tabelLabel" >Transactions</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

    async populateWeatherData() {
        const response = await App.fetchApi('api/Transactions');
        const data = await response.json();
        console.log(data);
        this.setState({ transactions: data, loading: false });
    }
}
