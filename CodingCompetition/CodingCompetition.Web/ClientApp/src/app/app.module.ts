import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AceEditorModule } from 'ng2-ace-editor';

import { MaterialModule } from './material.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ChallengeComponent } from './challenge/challenge.component';
import { AppRoutingModule } from './app-routing.module';
import { ApiService } from './shared/api.service';
import { ScoreListComponent } from './score-list/score-list.component';
import { ChallengeListComponent } from './challenge-list/challenge-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ChallengeComponent,
    NavMenuComponent,
    HomeComponent,
    ScoreListComponent,
    ChallengeListComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    RouterModule,
    FormsModule,
    AceEditorModule,
    MaterialModule,
    AppRoutingModule
  ],
  providers: [
    ApiService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
