import {Injectable} from '@angular/core';
import {DatePipe} from "@angular/common";

@Injectable({
  providedIn: 'root',
})
export class DateTimeService {

  constructor(private datePipe: DatePipe) { }

  convertDateToReadableFormat(isoDate: Date): string {
    const date = new Date(isoDate);

    if (this.isCreatedToday(date)) {
      return 'Today at ' + this.datePipe.transform(isoDate, 'HH:mm') ?? '-';
    }

    if (this.isYearsEqual(date)) {
      const dateWithOutYear = this.datePipe.transform(isoDate, 'MMM d, HH:mm');
      return dateWithOutYear == null ? '-' : dateWithOutYear;
    }

    const readableDate = this.datePipe.transform(isoDate, 'MMM d, y, HH:mm');
    return readableDate == null ? '-' : readableDate;
  }

  public getCurrentDateTime(): Date {
    const currentDate: Date = new Date();
    return new Date(Date.UTC(
      currentDate.getUTCFullYear(),
      currentDate.getUTCMonth(),
      currentDate.getUTCDate(),
      currentDate.getUTCHours(),
      currentDate.getUTCMinutes(),
      currentDate.getUTCSeconds(),
      currentDate.getUTCMilliseconds()
    ));
  }

  public isDateValid(date: Date): boolean {
    return !isNaN(date.getTime());
  }

  public isDateInFuture(date: Date): boolean {
    const currentDate = new Date();
    return date > currentDate;
  }

  private isCreatedToday(date: Date): boolean {
    const currentDate = new Date();

    return date.getDate() === currentDate.getDate()
      && date.getMonth() === currentDate.getMonth()
      && date.getFullYear() === currentDate.getFullYear();
  }

  private isYearsEqual(date: Date): boolean {
    const currentDate = new Date();
    return date.getFullYear() == currentDate.getFullYear();
  }
}
