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
    this.populateCategoryData();
  }

    static renderCategoriesTable(category: Category[]) {
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
      : FetchCategories.renderCategoriesTable(this.state.categories);

    return (
      <div>
        <h1 id="tabelLabel" >Categories</h1>
        {contents}
      </div>
    );
  }

    async populateCategoryData() {
        const response = await App.fetchApi('api/Categories');
        const data = await response.json();
        this.setState({ categories: data, loading: false });
    }
}
