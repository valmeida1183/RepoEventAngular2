import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu-superior',
  templateUrl: './menu-superior.component.html',
  styleUrls: []
})
export class MenuSuperiorComponent implements OnInit {
  // tslint:disable-next-line:no-inferrable-types
  isCollapsed: boolean = true;

  constructor() { }

  ngOnInit() {
  }

}
