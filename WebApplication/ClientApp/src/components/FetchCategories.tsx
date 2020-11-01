import React, { Component } from 'react';
import App from '../App';
import { Category } from './models/Category';

type State = {
    categories: string;
    loading: boolean;
};

export class FetchCategories extends Component {
    static displayName = FetchCategories.name;
    

    constructor(props: {}) {
        
    super(props);
      this.state = { category: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

    static renderForecastsTable(category: Category[]) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Title</th>
            <th>Dedicated amount</th>
          </tr>
        </thead>
        <tbody>
          {category.map(category =>
              <tr key={category.id}>
                  <td>{category.title}</td>
                  <td>{category.dedicatedamount}</td>
              </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchCategories.renderForecastsTable(this.state.categories);

    return (
      <div>
        <h1 id="tabelLabel" >Categories</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

    async populateWeatherData() {
        const response = await App.fetchApi('api/Categories');
        const data = await response.json();
        console.log(data);
        this.setState({ categories: data, loading: false });
    }
}
