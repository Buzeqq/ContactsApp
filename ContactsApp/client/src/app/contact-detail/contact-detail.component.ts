import {Component, OnInit} from '@angular/core';
import {ContactDetail} from "./contact-detail";
import {Observable} from "rxjs";
import {ContactsService} from "../contacts.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css']
})
export class ContactDetailComponent {
  public readonly contactDetail$ = this.contactsService.getContact(Number(this.route.snapshot.queryParamMap.get('id')!));

  constructor(private readonly contactsService: ContactsService, private readonly route: ActivatedRoute) { }

}
