import { Component, Input } from "@angular/core";
import { SearchResult } from '../shared/search-result.model';

@Component({
    selector: 'search-result',
    template: `
        <div class="container">
            <div class="row">
                 <div class="col-sm">
                    Name
                </div>
                <div class="col-sm">
                     Description
                </div>
            </div>
            <hr/>
            <div *ngFor="let searchResult of searchResults">
                <search-result-list-item [searchResult]="searchResult" class="row"></search-result-list-item>
                <hr/>
            </div>
        </div>
    `
})

export class SearchResultComponent{
    @Input() searchResults:SearchResult[];
}