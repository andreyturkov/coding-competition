import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';

import 'brace';
import 'brace/mode/csharp';
import 'brace/mode/java';

@Component({
  selector: 'challenge-list',
  templateUrl: './challenge-list.component.html',
  styleUrls: ['./challenge-list.component.css']
})
export class ChallengeListComponent implements OnInit {

  challenges: Challenge[];

  constructor(private http: ApiService) {
  }

  ngOnInit(): void {
    this.http.get<Challenge[]>('Challenge').subscribe(result => {
      this.challenges = result;
    });
  }
}
