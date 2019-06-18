import { Component, Input } from "@angular/core";
import { SearchResult } from '../shared/search-result.model';

@Component({
    selector: 'search-result',
    template: `
        <div class="container">
            <div *ngFor="let searchResult of searchResults" class="row">
                <search-result-list-item [searchResult]="searchResult"></search-result-list-item>
            </div>
        </div>
    `
})

export class SearchResultComponent{
    @Input() searchResults:SearchResult[];
}