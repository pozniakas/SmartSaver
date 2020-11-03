import React, { Component } from 'react';
import { LineChart, Line } from 'recharts';

const data = [{name: 'Page A', uv: 400, pv: 2400, amt: 2400},{name: 'Page A', uv: 400, pv: 2400, amt: 2400},{name: 'Page A', uv: 400, pv: 2400, amt: 2400}];

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Smart Saver</h1>
        <LineChart width={400} height={400} data={data}>
        <Line type="monotone" dataKey="uv" stroke="#8884d8" />
        </LineChart>
      </div>
    );
  }
}
