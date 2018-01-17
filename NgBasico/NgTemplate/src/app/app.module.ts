// Importação das dependências
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { CollapseModule } from 'ngx-bootstrap/collapse';

// Importação dos componentes
import { AppComponent } from './app.component';


@NgModule({
  declarations: [
    // Aqui vão as minhas dependêcias, como components, services etc..
    AppComponent
  ],
  imports: [
    // Aqui vão as dependências do próprio Angular.
    BrowserModule,
    FormsModule,
    HttpModule,
    CollapseModule.forRoot() // Alguns módulos precisam chamar esse método para entender que são chamados pelo módulo raiz.
  ],
  providers: [
    // Aqui são outros tipos de provedores.
  ],
  bootstrap: [
    // Aqui eu declaro o componente que irá inicializar a aplicação.
    AppComponent
  ]
})
export class AppModule { }
