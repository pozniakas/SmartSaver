import React, { Component } from 'react';
import { LineChart, Line } from 'recharts';

import {
  PieChart, Pie, Legend, Tooltip,
} from 'recharts';

const data01 = [
  { name: 'Groceries', value: 400 },
  { name: 'Wage & Salary', value: 300 },
  { name: 'Transport', value: 278 },
  { name: 'Entertainment', value: 50 },
];

const data = [{name: 'Page A', uv: 400, pv: 2400, amt: 2400},{name: 'Page A', uv: 400, pv: 2400, amt: 2400},{name: 'Page A', uv: 400, pv: 2400, amt: 2400}];

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (

      <div className = "App">
        <h1>Smart Saver</h1>
        <LineChart width={500} height={500} data={data}>
        <Line type="monotone" dataKey="uv" stroke="#8884d8" />
        </LineChart>
        <h2>Categories chart (eur.)</h2>
        <PieChart width={400} height={400}>
        <Pie dataKey="value" isAnimationActive={false} data={data01} cx={200} cy={200} outerRadius={80} fill="#8884d8" label />
        <Tooltip />
      </PieChart>
      <script src="./app.js"></script>
      </div>
    );
  }
}
