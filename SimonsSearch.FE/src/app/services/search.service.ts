import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class SearchService {
    searchResultApi = 'https://localhost:44375/api/search';

    constructor(private http: HttpClient) { }

    getSearchResult(term:string) : Observable<SearchResult[]> {
        return this.http.get<SearchResult[]>(this.searchResultApi, {params: {term: term}});
    }
}

interface SearchResult {
    Id: string;
    Description: string;
    Name: string;
    Weight: number;
}