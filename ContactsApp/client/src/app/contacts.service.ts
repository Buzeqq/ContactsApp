import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, tap} from "rxjs";
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
      this.contacts$.next(contacts);
    });
  }

  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>("/api/contacts");
  }

  getContact(id: number): Observable<ContactDetail> {
    return this.http.get<ContactDetail>("/api/contacts/" + id);
  }

  createContact(contact: ContactDetail): Observable<never> {
    if (contact.subCategory === undefined) {
      contact.subCategory = "";
    }

    return this.http.post<never>("/api/contacts", contact).pipe(
      tap(_ => {
        this.getContacts().subscribe(contacts => {
          this.contacts$.next(contacts);
        })
      })
    );
  }

  deleteContact(contactId: number): Observable<never> {
    return this.http.delete<never>("/api/contacts/" + contactId).pipe(
      tap(_ => {
        this.getContacts().subscribe(contacts => {
          this.contacts$.next(contacts);
        })
      })
    );
  }

  get contacts() {
    return this.contacts$.asObservable();
  }
}
