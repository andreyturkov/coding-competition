import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ScoreListComponent } from './score-list/score-list.component';
import { ChallengeComponent } from './challenge/challenge.component';
import { Error404Component } from './error404/error404.component';
import { SharedPadListComponent } from './shared-pad-list/shared-pad-list.component';
import { SharedPadComponent } from './shared-pad/shared-pad.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'score-list', component: ScoreListComponent },
  { path: 'challenge/:id', component: ChallengeComponent },
  { path: 'shared-pad-list', component: SharedPadListComponent },
  { path: 'shared-pad/:id', component: SharedPadComponent },
  { path: '**', component: Error404Component }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
  ]
})

export class AppRoutingModule {
}
