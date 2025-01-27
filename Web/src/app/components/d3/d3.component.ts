import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import * as d3 from 'd3';

@Component({
  selector: 'app-d3',
  imports: [FormsModule],
  templateUrl: './d3.component.html',
  styleUrl: './d3.component.css'
})
export class D3Component  {
  public inputData: string = '';
  @Output() closeModal = new EventEmitter<any>();

  public plotBarChart(): void {
    d3.select('#chart').selectAll('*').remove();

    const data = this.inputData
      .split(',')
      .map(d => parseFloat(d.trim()))
      .filter(d => !isNaN(d));

    if (data.length === 0) {
      alert('Enter at least one valid number.');
      return;
    }

    const width = 500;
    const height = 500;
    const margin = { top: 20, right: 20, bottom: 40, left: 40 };

    const svg = d3
      .select('#chart')
      .append('svg')
      .attr('width', width)
      .attr('height', height);

    const x = d3
      .scaleLinear()
      .domain([0, d3.max(data)!])
      .range([margin.left, width - margin.right]);

    const y = d3
      .scaleBand()
      .domain(data.map((_, i) => i.toString()))
      .range([height - margin.bottom, margin.top])
      .padding(0.1);

    //Bar
    svg
      .selectAll('.bar')
      .data(data)
      .enter()
      .append('rect')
      .attr('class', 'bar')
      .attr('x', x(0))
      .attr('y', (_, i) => y(i.toString())!)
      .attr('width', d => x(d) - x(0))
      .attr('height', y.bandwidth())
      .attr('fill', (_, i) => d3.schemeCategory10[i % 10]);

    //Label

    svg
      .selectAll('.bar-label')
      .data(data)
      .enter()
      .append('text')
      .attr('class', 'bar-label')
      .attr('x', d => x(d) + 5)
      .attr('y', (_, i) => y(i.toString())! + y.bandwidth() / 2)
      .attr('dy', '0.35em')
      .text(d => d.toString())
      .attr('fill', 'black')
      .attr('font-size', '12px');

    svg.append('g')
      .attr('transform', `translate(0,${height - margin.bottom})`)
      .call(d3.axisBottom(x).ticks(5).tickSize(0))
      .selectAll('*')
      .remove();

      svg.append('g')
      .attr('transform', `translate(${margin.left},0)`)
      .call(d3.axisLeft(y).tickSize(0))
      .selectAll('*')
      .remove();
  }

  public close(): void {
    this.closeModal.emit();
  }

}
