import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ApiService } from '../shared/api.service';
import { CodePad } from '../models/shared-pad/code-pad.model';

@Component({
  selector: 'shared-pad-list',
  templateUrl: './shared-pad-list.component.html',
  styleUrls: ['./shared-pad-list.component.css']
})
export class SharedPadListComponent implements OnInit {

  pads: CodePad[];
  isCreating: boolean;
  constructor(private http: ApiService, private router: Router) {
  }

  ngOnInit(): void {
    this.http.get<CodePad[]>('SharedPad').subscribe(result => {
      this.pads = result;
    });
  }

  createPad(): void {
    this.isCreating = true;
    this.http.post<any>('SharedPad', null).subscribe(result => {
      this.pads.push(result.id);
      this.router.navigateByUrl(`/shared-pad/${result.id}`);
      this.isCreating = false;
    });
  }

  removePad(index: number): void {
    const pad = this.pads[index];
    this.http.delete<boolean>(`SharedPad/${pad.id}`).subscribe(result => {
      if (result) {
        this.pads.splice(index);
      }
    });
  }
}
