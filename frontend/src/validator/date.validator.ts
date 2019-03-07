import { AbstractControl } from '@angular/forms';

export function dateTodayOrAfterValidator(control: AbstractControl): {[key: string]: any} | null {
  if (control.value === '' || control.value === undefined) {
    return null;
  }
  const currentDate = new Date();
  const date = new Date(control.value);
  date.setDate(date.getDate() + 1);
  const forbidden = date < currentDate;
  return forbidden ? {datePast: {value: control.value}} : null;
}
