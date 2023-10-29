import { DatePipe } from '@angular/common';
import { Component , OnInit, Input} from '@angular/core';
import { filterParameter } from 'src/app/Dto/Exclude/FilterParameter';
import { tagValueModel } from 'src/app/models/tagValueModel';
import { ReportFilterService } from 'src/app/services/report-filter.service';

interface Hall {
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

  halls: Hall[] | undefined;
  selectedHall: Hall | undefined;

  periods: Hall[] | undefined;
  selectedPeriod: Hall | undefined;

  @Input()
  selectedMeter:string|undefined;

  tagValues: tagValueModel[]=[];

  loading: boolean = false;

  constructor(private reportFilterService: ReportFilterService, private datepipe:DatePipe)
  {
  }

  ngOnInit(): void {
    this.halls = [
      { name: 'سالن رنگ 1', code: 'color1' },
      { name: 'سالن رنگ 2', code: 'color2' },
      { name: 'سالن رنگ 3', code: 'color3' },
      { name: 'سالن رنگ 4', code: 'color4' },
      { name: 'سالن رنگ 5', code: 'color5' }
  ];

  this.periods = [
    { name: 'ساعتی', code: 'hour' },
    { name: 'روزانه', code: 'day' },
    { name: 'ماهانه', code: 'month' }
    
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

    var parameter: filterParameter={
      hallName: this.selectedHall?.code,
      startDate: this.datepipe.transform(this.startDate, "yyyy-MM-dd"),
      endDate: this.datepipe.transform(this.endDate, "yyyy-MM-dd"),
      meter:3,
      period: this.selectedPeriod?.code,
    }

      this.reportFilterService.getTagValueByFilter(parameter).subscribe({
    next:(tagValues)=>{
      this.tagValues=tagValues;
    },
    error:(response)=>{
      console.log(response);
    }
    })
  }


  setChartData()
  {    
      
              const documentStyle = getComputedStyle(document.documentElement);
              const textColor = documentStyle.getPropertyValue('--text-color');
              const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
              const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
      
              this.basicData = {
                  labels: ['Q1', 'Q2', 'Q3', 'Q4'],
                  datasets: [
                      {
                          label: 'Sales',
                          data: [540, 325, 702, 620],
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
