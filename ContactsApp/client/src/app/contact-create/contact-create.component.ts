import { Component } from '@angular/core';
import {FormControl, NgForm} from "@angular/forms";
import {CategoryService} from "../category.service";
import {map, Observable, startWith, switchMap} from "rxjs";
import {MatOptionSelectionChange} from "@angular/material/core";
import {ContactsService} from "../contacts.service";
import {ContactDetail} from "../contact-detail/contact-detail";
import {MatChipInputEvent} from "@angular/material/chips";

@Component({
  selector: 'app-contact-create',
  templateUrl: './contact-create.component.html',
  styleUrls: ['./contact-create.component.css']
})
export class ContactCreateComponent {
  categories$: Observable<string[]> = this.categoryService.categories;
  subcategories$: Observable<string[]> = this.categoryService.getSubcategories('');
  chosenCategory?: string;
  chosenSubcategory?: string;

  contactForm?: ContactDetail;
  filteredSubCategories: Observable<string[]>;
  subcategoryCtrl = new FormControl<string>('');
  constructor(
    public readonly categoryService: CategoryService,
    private readonly contactService: ContactsService
  ) {
    this.filteredSubCategories = this.subcategoryCtrl.valueChanges.pipe(
      startWith(null),
      switchMap((typeAheadValue: string | null) => typeAheadValue === null
        ? this.subcategories$ // No filtering if user did not type anything
        : this.subcategories$.pipe(
          map(x => x.filter(c => c.toLocaleLowerCase().includes(typeAheadValue.toLocaleLowerCase())))
        )
    ));
  }

  create(form: NgForm) {
    this.contactForm = form.value;
    this.contactService.createContact(this.contactForm!).subscribe();
  }

  selectionChange($event: MatOptionSelectionChange) {
    if (!$event.isUserInput) return;
    this.subcategories$ = this.categoryService.getSubcategories($event.source.value);
  }

  createNewSubCategory($event: MatChipInputEvent) {
    const value = $event.value.trim();
    if (!value) return;

    this.categoryService.createSubcategory('other', value).subscribe();

    this.setSelectedSubCategory(value);
  }

  setSelectedSubCategory(chosenValue: string) {
    this.chosenSubcategory = chosenValue;
  }
}
