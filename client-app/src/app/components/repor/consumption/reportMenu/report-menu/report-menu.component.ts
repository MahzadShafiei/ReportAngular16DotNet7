import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-report-menu',
  templateUrl: './report-menu.component.html',
  styleUrls: ['./report-menu.component.css']
})

export class ReportMenuComponent implements OnInit {
    items: MenuItem[]=[];
    meter:string='';
    fromDate:Date | undefined;
    toDate:Date | undefined;
    hallName:string='';

    ngOnInit() {
        this.items = [
            {
                label: 'میزان مصرف',
                icon: 'pi pi-fw pi-file',
                items: [
                    {
                        label: 'برق',
                        command: () => {
                          this.updateMeter('El');
                      }
                        
                    },
                    {
                        label: 'آب ',
                        command: () => {
                          this.updateMeter('Wa');
                      }                       
                    },
                    { 
                        separator: true 
                    },
                    {
                        label: 'گاز',
                        command: () => {
                          this.updateMeter('Ga');
                      }
                    }
                ]
            }
        ];
    }

    updateMeter(meter: string) {
    this.meter=meter;
    console.log(this.meter);
  }
  
}