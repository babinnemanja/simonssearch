import { Component } from '@angular/core';

@Component({
  selector: 'search-app',
  template:  `
  <router-outlet></router-outlet>`
})
export class SearchAppComponent {
  title = 'Simmons Search';
}