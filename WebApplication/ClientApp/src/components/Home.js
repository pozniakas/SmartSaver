import React, { Component } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis,ReferenceLine } from 'recharts';

import {
  PieChart, Pie, Legend, Tooltip,
} from 'recharts';

const data01 = [
  { name: 'Groceries', value: 400 },
  { name: 'Wage & Salary', value: 300 },
  { name: 'Transport', value: 278 },
  { name: 'Entertainment', value: 50 },
];

const data = [
  {name: 'oct 1', amt: -300},
  {name: 'oct 2', amt: -120},
  {name: 'oct 3', amt: 2400}
];

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (

      <div className = "App">
        <h1>Smart Saver</h1>
        <h2 style={{textAlignVertical: "center",textAlign: "center",}}>Overview</h2>
      
        <h2>Income and expenses</h2>
        <LineChart width={400} height={400} data={data} float="left">
        <Line type="monotone" dataKey="amt" stroke="#8884d8" />
        <CartesianGrid strokeDasharray="3 3" />
        <Tooltip />
        <XAxis dataKey="name" tickLine={false} />
        <ReferenceLine y={0} stroke="#000000" />
        <YAxis />
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
