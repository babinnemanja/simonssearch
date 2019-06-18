import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule }   from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { SearchService } from './services/search.service';
import { HttpClientModule } from '@angular/common/http'
import { SearchResultListComponent } from './search-result-list-item/search-result-list-item.component';
import { SearchResultComponent } from './search-result/search-result.component';
import { SearchFormComponent } from './search-form/search-form.component';
import { SearchAppComponent } from './app-search.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

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
    HttpClientModule,
    NoopAnimationsModule,
    FormsModule
  ],
  providers: [
    SearchService
  ],
  bootstrap: [SearchAppComponent]
})
export class AppModule { }
