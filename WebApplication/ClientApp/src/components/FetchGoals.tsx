import React, { Component } from 'react';
import App from '../App';
import { Goal } from './models/Goal';

type State = {
    goals: string;
    loading: boolean;
};

export class FetchGoals extends Component {
    static displayName = FetchGoals.name;
    

    constructor(props: {}) {
        
    super(props);
      this.state = { category: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

    static renderForecastsTable(category: Goal[]) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Title</th>
            <th>Description</th>
            <th>Amount</th>
            <th>Creation date</th>
            <th>Deadline date</th>
          </tr>
        </thead>
        <tbody>
          {category.map(category =>
              <tr key={category.id}>
                  <td>{category.title}</td>
                  <td>{category.description}</td>
                  <td>{category.amount}</td>
                  <td>{category.creationdate}</td>
                  <td>{category.deadlinedate}</td>
              </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchGoals.renderForecastsTable(this.state.goals);

    return (
      <div>
        <h1 id="tabelLabel" >Goals</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

    async populateWeatherData() {
        const response = await App.fetchApi('api/Goals');
        const data = await response.json();
        console.log(data);
        this.setState({ goals: data, loading: false });
    }
}
