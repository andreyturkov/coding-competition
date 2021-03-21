import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ScoreListComponent } from './score-list/score-list.component';
import { ChallengeComponent } from './challenge/challenge.component';


const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'score-list', component: ScoreListComponent },
  { path: 'challenge/:id', component: ChallengeComponent }
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
