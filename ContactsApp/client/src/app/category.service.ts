import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private readonly categories$ = new BehaviorSubject<string[]>([]);

  constructor(private readonly http: HttpClient) {
    this.getCategories().subscribe(categories => {
      this.categories$.next(categories);
    });
  }

  getCategories(): Observable<string[]> {
    return this.http.get<string[]>("/api/categories");
  }

  getSubcategories(category: string): Observable<string[]> {
    return this.http.get<string[]>("/api/categories/" + category);
  }

  createSubcategory(category: string, subcategory: string): Observable<never> {
    return this.http.post<never>(`/api/categories/${category}`, subcategory);
  }

  get categories() {
    return this.categories$.asObservable();
  }
}
