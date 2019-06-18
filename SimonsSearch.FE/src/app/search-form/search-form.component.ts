import { Component, Input } from "@angular/core";
import { SearchResult } from '../shared/search-result.model';
import { SearchService } from '../services/search.service';
import {map} from 'rxjs/operators'

@Component({
    selector: 'search-form',
    template: `
    <form #searchForm="ngForm" (ngSubmit)="search(searchForm.value)">
         <input (ngModel)="searchText" type="text" placeholder="Search" aria-label="Search"/>
          <button type="submit">Search</button>
    </form>
    <search-result [searchResults]="searchResults"></search-result>
    `
})

export class SearchFormComponent{
    searchText;
    searchResults: SearchResult[];

    constructor(private searchService: SearchService){

    }

    search(form){
        this.searchService.getSearchResult(form.searchText).subscribe((data) => {
            this.searchResults = data;
        });
    }
}