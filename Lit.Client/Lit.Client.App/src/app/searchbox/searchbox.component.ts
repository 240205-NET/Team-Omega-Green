import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-searchbox',
  templateUrl: './searchbox.component.html',
  styleUrls: ['./searchbox.component.css']
})
export class SearchboxComponent {
  @Input() router!: Router;
  searching: boolean = false; //set searching is not in progress

  constructor() {}

  searchDatabase(searchText: string) {
    console.log(searchText);
    if (this.searching || !searchText.trim()) {
      //If searching is already in progress or searchValue is empty, DON'T DO ANYTHING
      return;
    }
    //Set searching to TRUE to indicate that navigation is in progress.. I was getting errors with loading components without this check
    this.searching = true;

    //Navigate to search results component with the search value as a parameter!!!!!!!!!!!
    this.router.navigate(['/search-results', searchText])
      .then(() => {
        // Navigation is successful then set searching to false!!!!!!!!!!!!!!!!!!!!!
        this.searching = false;
      })
      .catch((error) => {
        // Error handling
        console.error('Navigation error:', error);
        // Set searching to false to enable the button again :D
        this.searching = false;
      });
}}