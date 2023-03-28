import { Component } from '@angular/core';
import {ContactsService} from "../contacts.service";
import {Observable} from "rxjs";
import {Contact} from "./contact";

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent {
  contacts$: Observable<Contact[]>;
  constructor(public readonly contactsService: ContactsService) {
    this.contacts$ = contactsService.contacts;
  }

}
