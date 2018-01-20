import { Component, OnInit } from '@angular/core';
import { SeoService, SeoModel } from '../services/seo.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(seoService: SeoService) {
    // tslint:disable-next-line:prefer-const
    let seoModel: SeoModel = <SeoModel>{
      title: 'Seja bem vindo',
      robots: 'Index,Follow'
    };

    seoService.setSeoData(seoModel);
  }

  ngOnInit() {
  }

}
