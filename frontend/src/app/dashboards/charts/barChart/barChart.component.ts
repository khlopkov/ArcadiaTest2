import {ChangeDetectionStrategy, Component, ElementRef, Input, ViewChild, OnInit, OnChanges} from '@angular/core';
import * as d3 from 'd3';

import { BarDataModel } from '../models/bar.model';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './barChart.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BarChartComponent implements OnChanges {
  @ViewChild('barChart')
  chartElement: ElementRef;

  @Input() data: BarDataModel[];

  constructor() { }

  ngOnChanges(): void {
    if (this.data) {
      this.build();
    }
  }

  build() {
    const margin = {top: 20, right: 20, bottom: 70, left: 40};
    const width = 500 - margin.left - margin.right;
    const height = 300 - margin.top - margin.bottom;

    const x = d3.scaleBand().range([0, width]);

    const y = d3.scaleLinear().range([height, 0]);

    const xAxis = d3.axisBottom(x);
    const yAxis = d3.axisLeft(y).ticks(d3.max(this.data, d => d.y));

    d3.select(this.chartElement.nativeElement).select('svg').remove();
    const svg = d3.select(this.chartElement.nativeElement).append('svg')
      .attr('width', width + margin.left + margin.right)
      .attr('height', height + margin.top + margin.bottom)
      .append('g')
      .attr('transform',
          'translate(' + margin.left + ',' + margin.top + ')');

    x.domain(this.data.map(d => d.x ));
    y.domain([0, d3.max(this.data, d => d.y)]);

    svg.append('g')
      .attr('class', 'x axis')
      .attr('transform', 'translate(0,' + height + ')')
      .call(xAxis)
    .selectAll('text')
      .style('text-anchor', 'end')
      .attr('dx', '-.8em')
      .attr('dy', '-.55em')
      .attr('transform', 'rotate(-90)' );

    svg.append('g')
      .attr('class', 'y axis')
      .call(yAxis)
    .append('text')
      .attr('transform', 'rotate(-90)')
      .attr('y', 6)
      .attr('dy', '.71em')
      .style('text-anchor', 'end')
      .text('Value ($)');

    svg.selectAll('bar')
      .data(this.data)
    .enter().append('rect')
      .style('fill', 'steelblue')
      .attr('x', d => x(d.x))
      .attr('width', x.bandwidth())
      .attr('y', d => y(d.y))
      .attr('height', d => height - y(d.y));

  }
}
