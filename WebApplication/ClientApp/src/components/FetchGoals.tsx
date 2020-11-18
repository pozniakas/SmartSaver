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
      this.state = { goals: [], loading: true };
  }

  componentDidMount() {
    this.populateGoalData();
  }

    static renderGoalsTable(goal: Goal[]) {
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
          {goal.map(goal =>
              <tr key={goal.id}>
                  <td>{goal.title}</td>
                  <td>{goal.description}</td>
                  <td>{goal.amount}</td>
                  <td>{goal.creationdate}</td>
                  <td>{goal.deadlinedate}</td>
              </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchGoals.renderGoalsTable(this.state.goals);

    return (
      <div>
        <h1 id="tabelLabel" >Goals</h1>
        {contents}
      </div>
    );
  }

    async populateGoalData() {
        const response = await App.fetchApi('api/Goals');
        const data = await response.json();
        this.setState({ goals: data, loading: false });
    }
}
