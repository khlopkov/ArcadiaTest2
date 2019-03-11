import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { PopupComponent } from './popup/popup.component';

@NgModule({
  declarations: [ PopupComponent ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  exports: [ PopupComponent ],
  bootstrap: []
})
export class RepresentationModule { }

