import { Component, OnInit } from "@angular/core";
import { SearchResult } from '../shared/search-result.model';
import { SearchService } from '../services/search.service';

@Component({
    selector: 'search-form',
    templateUrl: './search-form.component.html'
})

export class SearchFormComponent implements OnInit{
    searchText;
    hasData:boolean;
    searchTriggered:boolean;
    searchResults: SearchResult[];

    constructor(private searchService: SearchService){

    }

    ngOnInit() {
        this.searchTriggered = false;
      }

    search(form){
        this.searchService.getSearchResult(form.searchText).subscribe((data) => {
            this.searchResults = data;
            this.hasData = data !== undefined && data.length !== 0;
            this.searchTriggered = true;
        });
    }
}