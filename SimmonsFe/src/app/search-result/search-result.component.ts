import { Component, Input } from "@angular/core";
import { SearchResult } from '../shared/search-result.model';

@Component({
    selector: 'search-result',
    templateUrl: './search-result.component.html'
})

export class SearchResultComponent{
    @Input() searchResults:SearchResult[];
}