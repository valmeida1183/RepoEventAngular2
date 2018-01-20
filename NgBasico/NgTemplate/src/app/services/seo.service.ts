import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ɵgetDOM as getDOM } from '@angular/platform-browser'; // para trabalhar com a manipalçao do DOM.
import { StringUtils } from '../utils/string.utils';

@Injectable()
export class SeoService {
    private titleService: Title;
    private headElement: HTMLElement;
    private metaDescription: HTMLElement;
    private metaKeywords: HTMLElement;
    private robots: HTMLElement;
    private DOM: any;

    public constructor(titleService: Title) {
        this.titleService = titleService;
        this.DOM = getDOM();
        this.headElement = this.DOM.query('head');
        this.setTitle('');
    }

    public setSeoData(seoModel: SeoModel) {
        this.setTitle(seoModel.title);
    }

    private setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle + ' - Eventos.IO');
    }

    private setMetaDescription(description: string) {
        this.metaDescription = this.getOrCreateMetaElement('description');
        if (StringUtils.isNullOrEmpty(description)) { description = 'Aqui você encontra um evento técnico próximo de você'; }
        this.metaDescription.setAttribute('content', description);
    }

    private setMetaKeywords(keywords: string) {
        this.metaKeywords = this.getOrCreateMetaElement('keywords');
        if (StringUtils.isNullOrEmpty(keywords)) { keywords = 'eventos,workshops,encontros,congressos,comunidades,tecnologia'; }
        this.metaKeywords.setAttribute('content', keywords);
    }

    private setMetaRobots(robots: string) {
        this.robots = this.getOrCreateMetaElement('robots');
        if (StringUtils.isNullOrEmpty(robots)) { robots = 'all'; }
        this.robots.setAttribute('content', robots);
    }

    private getOrCreateMetaElement(name: string): HTMLElement {
        let el: HTMLElement;
        el = this.DOM.query('meta[name=' + name + ']');
        if (el === null) {
            el = this.DOM.createElement('meta');
            el.setAttribute('name', name);
            this.headElement.appendChild(el);
        }
        return el;
    }
}

export class SeoModel {
    public title = '';
    public description = '';
    public robots = '';
    public keywords = '';
}
