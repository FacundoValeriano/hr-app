import { Component, Input } from '@angular/core';
import { forEach } from '@angular/router/src/utils/collection';
import * as  differenceInCalendarDays from 'date-fns/difference_in_calendar_days';
import { DaysOff } from 'src/entities/days-off';

@Component({
  selector: 'nz-basic-calendar',
  template: `
  <div nz-row>
    <div >
      <div [ngStyle]="{ width: '400px', border: '1px solid #d9d9d9', borderRadius: '4px'}">
        <nz-calendar
          nzCard
          [(ngModel)]=selectedDate
          (nzSelectChange)="onValueChange($event)"
          (nzPanelChange)="onPanelChange($event)"
        ></nz-calendar>
      </div>
    </div>
    <div>
      <ul *ngFor="let item of _listOfCompanyCalendar">
        <div *ngIf= "selectedDateString == item.date.substr(0, 11)">
          <div *ngIf="item.type == 'Festivity'">
           <li style="font-weigth: bold; font-size: 18px; color:rgb(179, 22, 179);">{{item.type}}: {{item.comments}}</li>
          </div>
          <div *ngIf="item.type == 'Reminder'">
           <li style="font-weigth: bold; font-size: 18px; color: grey;">{{item.type}}: {{item.comments}}</li>
          </div>
        </div>
      </ul>
    </div>
    <div>
    <ul *ngFor="let item2 of _listOfDaysOff">
      <div *ngIf= "askForDate(item2)">
          <div *ngIf="item2.status == 'Holidays'">
          <li style="font-weigth: bold; font-size: 18px; color:darkolivegreen;">{{item2.type}}}</li>
          </div>
          <div *ngIf="item2.type == 'PTO'">
          <li style="font-weigth: bold; font-size: 18px; color: firebrick;">{{item2.type}}</li>
          </div>
          <div *ngIf="item2.type == 'Study days'">
          <li style="font-weigth: bold; font-size: 18px; color: mediumblue;">{{item2.type}}</li>
          </div>
          <div *ngIf="item2.type == 'Training'">
          <li style="font-weigth: bold; font-size: 18px; color: maroon;">{{item2.type}}</li>
          </div>
          <div *ngIf="item2.type != 'Holidays' && item2.type != 'PTO' && item2.type != 'Study days' && item2.type != 'Training'">
          <li style="font-weigth: bold; font-size: 18px; color: black;">{{item2.type}}</li>
          </div>
      </div>
    </ul>
  </div>

  </div>
  `
})
export class NzCalendarComponent {
  @Input() _listOfCompanyCalendar;
  @Input() _listOfDaysOff;

  selectedDate : Date;
  selectedDateString : string;
  currentDate : Date = new Date();
  
  ngOnInit() {
    this.selectedDateString = this.currentDate.toISOString();
    this.selectedDateString = this.selectedDateString.substr(0, 11);
  }

  askForDate(dayOff : DaysOff) : boolean {
    // let TotalDaysOff = this._listOfDaysOff.length;
    // for (var i = 0; i <= TotalDaysOff; i++) {      
      let rangeDays =  differenceInCalendarDays(dayOff.endDate, dayOff.date);
      let indexDate = new Date(dayOff.date);
       for (let j :number = 0; j <= rangeDays; j++) {      
          indexDate.setDate(indexDate.getDate() + j); 
          let indexDateString : String = indexDate.toISOString().substr(0, 11); console.log(indexDateString + "la del rango"); console.log(this.selectedDateString);
          if(indexDateString == this.selectedDateString) return true;
       }
       return false;
    // }
  }

  onValueChange(value: Date): void {
    this.selectedDateString = this.selectedDate.toISOString();
    this.selectedDateString = this.selectedDateString.substr(0, 11);
  }

  onPanelChange(change: { date: Date; mode: string }): void {
    console.log(`Current value: ${change.date}`);
    console.log(`Current mode: ${change.mode}`);
  }

//check in
}