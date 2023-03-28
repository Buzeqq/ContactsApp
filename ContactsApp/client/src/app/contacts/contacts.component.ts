import { Component } from '@angular/core';
import {ContactsService} from "../contacts.service";
import {Observable} from "rxjs";
import {Contact} from "./contact";
import { AuthenticationService } from '../authentication.service';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent {
  contacts$: Observable<Contact[]>;
  constructor(
    public readonly contactsService: ContactsService,
    public readonly authService: AuthenticationService,
    private readonly router: Router,
    private readonly route: ActivatedRoute) {
    this.contacts$ = contactsService.contacts;
  }

  delete(id: number) {
    this.contactsService.deleteContact(id).subscribe();
  }
}
