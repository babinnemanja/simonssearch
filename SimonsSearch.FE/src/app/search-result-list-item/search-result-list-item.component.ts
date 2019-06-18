import { Component, Input } from "@angular/core";
import { SearchResult } from '../shared/search-result.model';

@Component({
    selector: 'search-result-list-item',
    templateUrl: './search-result-list-item.component.html'
})

export class SearchResultListComponent{
    @Input() searchResult:SearchResult;
}