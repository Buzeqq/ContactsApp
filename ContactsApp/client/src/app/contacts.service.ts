import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from "rxjs";
import {Contact} from "./contacts/contact";
import {HttpClient} from "@angular/common/http";
import {ContactDetail} from "./contact-detail/contact-detail";

@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  private readonly contacts$ = new BehaviorSubject<Contact[]>([]);

  constructor(private readonly http: HttpClient) {
    this.getContacts().subscribe(contacts => {
      console.log(contacts);
      this.contacts$.next(contacts);
    });
  }

  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>("/api/contacts");
  }

  getContact(id: number): Observable<ContactDetail> {
    return this.http.get<ContactDetail>("/api/contacts/" + id);
  }

  get contacts() {
    return this.contacts$.asObservable();
  }
}
