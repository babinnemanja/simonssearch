import { Component, Input } from "@angular/core";
import { SearchResult } from '../shared/search-result.model';
import { SearchService } from '../services/search.service';

@Component({
    selector: 'search-form',
    template: `
    <form #searchForm="ngForm" (ngSubmit)="search(searchForm.value)" class="form-inline md-form mr-auto mb-4">
        <input (ngModel)="searchText" name="searchText" id="searchText" class="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search">
        <button mdbBtn size="sm" gradient="aqua" rounded="true" class="my-0 waves-light" mdbWavesEffect type="submit">Search</button>  
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
            console.info(data);
            this.searchResults = data;
        });
    }
}