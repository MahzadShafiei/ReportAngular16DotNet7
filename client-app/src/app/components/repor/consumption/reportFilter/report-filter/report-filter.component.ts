import { DatePipe } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { Meter, Period, filterParameter } from 'src/app/Dto/Exclude/FilterParameter';
import { ChartModel } from 'src/app/Dto/Include/ChartModel';
import { unitModel } from 'src/app/models/unitModel';
import { tagValueModel } from 'src/app/models/tagValueModel';
import { ReportFilterService } from 'src/app/services/report-filter.service';


interface DropDownPeriodType {
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
export class ReportFilterComponent implements OnInit {
  basicData: any;
  basicOptions: any;

  calculatedAssumption: any;

  startDate: Date = new Date();
  endDate: Date = new Date();

  assistanceTypes: unitModel[] = [];
  selectedAssistance: unitModel | undefined;

  managementTypes: unitModel[] = [];
  selectedManagement: unitModel | undefined;

  hallsType: unitModel[] | undefined;
  selectedHallType: unitModel | undefined;

  hallsCode: DropDownHallCode[] | undefined;
  selectedHallCode: DropDownHallCode | undefined;

  periods: DropDownPeriodType[] | undefined;
  selectedPeriod: DropDownPeriodType | undefined;

  selectedMeterTest: string = "";

  @Input()
  selectedMeter: string = "";

  tagValues: tagValueModel[] = [];
  chartData: ChartModel[] = [];

  loading: boolean = false;

  constructor(private reportFilterService: ReportFilterService, private datepipe: DatePipe) {
  }

  onChangeManagement() {
    this.reportFilterService.GetUnitsByParameter(Number(this.selectedManagement?.id)).subscribe({
      next: (management) => {
        this.hallsType = management;
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  onChangeAssistance() {
    this.reportFilterService.GetUnitsByParameter(Number(this.selectedAssistance?.id)).subscribe({
      next: (management) => {
        this.managementTypes = management;
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  onChangeHall() {
    this.hallsCode =
      [
        { name: '1', code: '1' },
        { name: '2', code: '2' },
        { name: '3', code: '3' },
        { name: '4', code: '4' },
        { name: '5', code: '5' }
      ];
  }

  ngOnInit(): void {

    this.reportFilterService.GetUnitsByParameter(1).subscribe({
      next: (assitances) => {
        console.log(assitances);
        this.assistanceTypes = assitances;
        return (assitances);
      },
      error: (response) => {
        console.log(response);
      }
    });

    this.periods = [
      { name: 'دقیقه ای', code: Period.Minute },
      { name: 'ساعتی', code: Period.Hour },
      { name: 'روزانه', code: Period.Day },
      //{ name: 'ماهانه', code: Period.Month }

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

  //ایجاد آبجکت پارامتر ورودی سرویس
  makeParameter() {
    var meter: Meter = Meter[this.selectedMeter as keyof typeof Meter];

    var parameter: filterParameter = {
      assisttanceType: this.selectedAssistance == undefined ? 0 : this.selectedAssistance?.id,
      managementType: this.selectedManagement == undefined ? 0 : this.selectedManagement?.id,
      hallType: this.selectedHallType == undefined ? 0 : this.selectedHallType?.id,
      hallCode: this.selectedHallCode == undefined ? '0' : this.selectedHallCode?.code,
      startDate: this.datepipe.transform(this.startDate, "yyyy-MM-dd"),
      endDate: this.datepipe.transform(this.endDate, "yyyy-MM-dd"),
      meter: meter,
      period: this.selectedPeriod?.code,
    }

    return parameter;
  }

  calculateAssumption() {
    var parameter = this.makeParameter();

    this.reportFilterService.getCalculatedAssumption(parameter).subscribe({
      next: (number) => {
        this.calculatedAssumption = number;
        console.log(number);
      },
      error: (response) => {
        console.log(response);
      }
    })
  }


  getByFilter() {

    var parameter = this.makeParameter();

    this.reportFilterService.getTagValueByFilter(parameter).subscribe({
      next: (chartData) => {
        this.chartData = chartData;
        console.log(this.chartData);
        this.setChartData(this.chartData);
      },
      error: (response) => {
        console.log(response);
      }
    })
  }


  setChartData(chartData: ChartModel[]) {

    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

    this.basicData = {
      //labels: ['Q1', 'Q2', 'Q3', 'Q4'],
      labels: chartData.map(c => c.label),
      datasets: [
        {
          label: 'Value Average',
          //data: [540, 325, 702, 620],
          data: chartData.map(c => c.data),
          backgroundColor: ['rgba(255, 159, 64, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(153, 102, 255, 0.2)'],
          borderColor: ['rgb(255, 159, 64)', 'rgb(75, 192, 192)', 'rgb(54, 162, 235)', 'rgb(153, 102, 255)'],
          borderWidth: 1,
          tension: 0.4,
          fill: false,
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
