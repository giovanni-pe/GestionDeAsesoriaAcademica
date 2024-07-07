import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

interface ResearchLine {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class ResearchLineService {
  private apiUrl = 'http://localhost:7000/api/v1/ResearchLine';

  constructor(private http: HttpClient) {}

  getResearchLines(): Observable<ResearchLine[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => response.data.items.map((item: any) => ({
        id: item.id,
        name: item.name
      }))),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('Error occurred:', error);
    return throwError('An error occurred while fetching data. Please try again later.');
  }
}
