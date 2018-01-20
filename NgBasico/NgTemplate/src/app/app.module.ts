// Importação das dependências
import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { rootRouterConfig } from './app.routes';

// Importação dos componentes do bootstrap
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { CarouselModule } from 'ngx-bootstrap/carousel';

// Importação dos componentes shared
import { MenuSuperiorComponent } from './shared/menu-superior/menu-superior.component';
import { FooterComponent } from './shared/footer/footer.component';
import { MainPrincipalComponent } from './shared/main-principal/main-principal.component';
import { MenuLoginComponent } from './shared/menu-login/menu-login.component';

// Importação dos componentes
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ListaEventosComponent } from './eventos/lista-eventos/lista-eventos.component';

// Importação dos Serviços
import { SeoService } from './services/seo.service';

@NgModule({
  declarations: [
    // Aqui vão as minhas dependêcias, como components.
    AppComponent,
    MenuSuperiorComponent,
    FooterComponent,
    MainPrincipalComponent,
    HomeComponent,
    MenuLoginComponent,
    ListaEventosComponent
  ],
  imports: [
    // Aqui vão as importações dos módulos.
    BrowserModule,
    FormsModule,
    HttpModule,
    CollapseModule.forRoot(), // Alguns módulos precisam chamar esse método para entender que são chamados pelo módulo raiz.
    CarouselModule.forRoot(),
    RouterModule.forRoot(rootRouterConfig, {useHash: false})
  ],
  providers: [
    // Aqui são outros tipos de provedores.
    Title,
    SeoService
  ],
  bootstrap: [
    // Aqui eu declaro o componente que irá inicializar a aplicação.
    AppComponent
  ]
})
export class AppModule { }
