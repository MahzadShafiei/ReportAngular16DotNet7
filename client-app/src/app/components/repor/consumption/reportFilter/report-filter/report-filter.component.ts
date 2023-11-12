import { DatePipe } from '@angular/common';
import { Component , OnInit, Input} from '@angular/core';
import { HallType, Meter, Period, filterParameter } from 'src/app/Dto/Exclude/FilterParameter';
import { ChartModel } from 'src/app/Dto/Include/ChartModel';
import { tagValueModel } from 'src/app/models/tagValueModel';
import { ReportFilterService } from 'src/app/services/report-filter.service';


interface DropDownHallType {
  name: string;
  code: HallType
}

interface DropDownPeriodType{
  name: string;
  code: Period;
}

interface DropDownHallCode {
  name: string;
  code: string;
}

@Component({
  selector: 'app-report-filter',
  templateUrl: './report-filter.component.html',
  styleUrls: ['./report-filter.component.css']
})
export class ReportFilterComponent  implements OnInit{
  basicData: any;
  basicOptions: any;

  startDate: Date = new Date();
  endDate: Date = new Date();

  hallsType: DropDownHallType[] | undefined;
  selectedHallType: DropDownHallType | undefined;

  hallsCode: DropDownHallCode[] | undefined;
  selectedHallCode: DropDownHallCode |undefined;

  periods: DropDownPeriodType[] | undefined;
  selectedPeriod: DropDownPeriodType | undefined;

  selectedMeterTest:string="";

  @Input()
  selectedMeter:string="";

  tagValues: tagValueModel[]=[];
  chartData: ChartModel[]=[];

  loading: boolean = false;

  constructor(private reportFilterService: ReportFilterService, private datepipe:DatePipe)
  {
  }

  ngOnInit(): void {
    this.hallsType = [
      { name: 'سالن رنگ', code: HallType.Paint },
      { name:'سالن بدنه', code: HallType.Body}      
  ];

  this.hallsCode=
  [
    {name: '1', code: '1'},
    {name: '2', code: '2'},
    {name: '3', code: '3'},
    {name: '4', code: '4'},
    {name: '5', code: '5'}
  ];

  this.periods = [
    { name: 'ساعتی', code: Period.Hour },
    { name: 'روزانه', code: Period.Day },
    { name: 'ماهانه', code: Period.Month }
    
  ];
  
  }

  //جست و جوی دیتای 
  load() {

    this.loading = true;
    this.getByFilter();

    setTimeout(() => {
        this.loading = false
    }, 2000);

  }

  getByFilter()
  {
    var a= this.selectedMeter;
    var meter : Meter = Meter[this.selectedMeter as keyof typeof Meter];
    
    var parameter: filterParameter={
      hallType: this.selectedHallType?.code,
      hallCode: this.selectedHallCode?.code,
      startDate: this.datepipe.transform(this.startDate, "yyyy-MM-dd"),
      endDate: this.datepipe.transform(this.endDate, "yyyy-MM-dd"),
      meter:meter,
      period: this.selectedPeriod?.code,
    }
    console.log(parameter);

      this.reportFilterService.getTagValueByFilter(parameter).subscribe({
    next:(chartData)=>{
      this.chartData=chartData;
      console.log(this.chartData);
      this.setChartData(this.chartData);
    },
    error:(response)=>{
      console.log(response);
    }
    })
  }


  setChartData(chartData: ChartModel[])
  {    
      
              const documentStyle = getComputedStyle(document.documentElement);
              const textColor = documentStyle.getPropertyValue('--text-color');
              const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
              const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
      
              this.basicData = {
                  //labels: ['Q1', 'Q2', 'Q3', 'Q4'],
                  labels: chartData.map(c=> c.label),
                  datasets: [
                      {
                          label: 'Sales',
                          //data: [540, 325, 702, 620],
                          data: chartData.map(c=> c.data),
                          backgroundColor: ['rgba(255, 159, 64, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(153, 102, 255, 0.2)'],
                          borderColor: ['rgb(255, 159, 64)', 'rgb(75, 192, 192)', 'rgb(54, 162, 235)', 'rgb(153, 102, 255)'],
                          borderWidth: 1
                      }
                  ]
              };
      
              this.basicOptions = {
                  plugins: {
                      legend: {
                          labels: {
                              color: textColor
                          }
                      }
                  },
                  scales: {
                      y: {
                          beginAtZero: true,
                          ticks: {
                              color: textColorSecondary
                          },
                          grid: {
                              color: surfaceBorder,
                              drawBorder: false
                          }
                      },
                      x: {
                          ticks: {
                              color: textColorSecondary
                          },
                          grid: {
                              color: surfaceBorder,
                              drawBorder: false
                          }
                      }
                  }
              };             
       
  }


}
