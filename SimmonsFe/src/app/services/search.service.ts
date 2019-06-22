import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SearchResult } from '../shared/search-result.model';

@Injectable()
export class SearchService {
    searchResultApi = 'https://simonssearchapi.azurewebsites.net/api/search';

    constructor(private http: HttpClient) { }

    getSearchResult(term:string) : Observable<SearchResult[]> {
        return this.http.get<SearchResult[]>(this.searchResultApi, {params: {term: term}});
    }
}