import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { SearchResultListComponent } from './search-result-list-item/search-result-list-item.component';
import { SearchResultComponent } from './search-result/search-result.component';
import { SearchFormComponent } from './search-form/search-form.component';
import { SearchAppComponent } from './app-search.component';
import { SearchService } from './services/search.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    SearchResultListComponent,
    SearchResultComponent,
    SearchFormComponent,
    SearchAppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MDBBootstrapModule.forRoot(),
    HttpClientModule,
    FormsModule
  ],
  providers: [
    SearchService
  ],
  bootstrap: [SearchAppComponent]
})
export class AppModule { }
