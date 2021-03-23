import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';

import { ApiService } from '../shared/api.service';
import { SignalRService } from '../shared/signal-r.service';
import { CodePad } from '../models/shared-pad/code-pad.model';
import { SharedPadUserPopupComponent } from '../shared-pad-user-popup/shared-pad-user-popup.component';
import { CodePadUser } from '../models/shared-pad/code-pad-user.model';
import { RunResult } from '../models/shared-pad/run-result.model';

@Component({
  selector: 'shared-pad',
  templateUrl: './shared-pad.component.html',
  styleUrls: ['./shared-pad.component.css']
})
export class SharedPadComponent implements OnInit, OnDestroy {

  private skipChange = false;
  isRunning = false;

  padId: string;
  codePad: CodePad;
  users: CodePadUser[];
  connectionEstablished: Observable<boolean>;
  runResult: RunResult;

  options: any = { printMargin: false, enableBasicAutocompletion: true };

  constructor(public dialog: MatDialog,
    private http: ApiService,
    private route: ActivatedRoute,
    private router: Router,
    private signalRService: SignalRService) {
    this.connectionEstablished = this.signalRService.connectionEstablished$;

    this.signalRService.messageReceived$.subscribe((message) => {
      this.skipChange = true;
      this.codePad = message;
    });
    this.signalRService.userJoined$.subscribe((message) => {
      this.users.push(message);
    });
    this.signalRService.userLeft$.subscribe((message) => {
      var index = this.users.findIndex(x => x.id === message);
      if (index > -1) {
        this.users.splice(index, 1);
      }
    });
    this.signalRService.codePadRemoved$.subscribe((padId) => {
      if (this.padId === padId) {
        this.router.navigateByUrl('/shared-pad-list');
      }
    });
    this.signalRService.padRunning$.subscribe((result) => {
      this.isRunning = result;
    });
    this.signalRService.padRun$.subscribe((result) => {
      this.runResult = result;
    });
  }

  ngOnInit(): void {
    this.connectionEstablished.subscribe((result) => {
      if (result) {
        const user = localStorage.getItem('shared-pad-user');
        if (!user) {
          this.openUserDialog();
        } else {
          this.signalRService.joinCodePad(this.padId, user);
        }
      }
    });

    this.route.params.subscribe(params => {
      this.padId = params['id'];

      this.http.get<CodePad>(`SharedPad/${this.padId}`).subscribe(result => {
        this.codePad = result;
      });
      this.http.get<CodePadUser[]>(`SharedPad/${this.padId}/Users`).subscribe(result => {
        this.users = result;
      });
    });

  }

  ngOnDestroy(): void {
    if (this.signalRService.isConnected) {
      this.signalRService.leaveCodePad(this.padId);
    }
    localStorage.removeItem('shared-pad-user');
  }

  openUserDialog(): void {
    const dialogRef = this.dialog.open(SharedPadUserPopupComponent, { width: '250px' });

    dialogRef.afterClosed().subscribe(() => {
      const user = localStorage.getItem('shared-pad-user');
      if (!user) {
        this.router.navigateByUrl('/shared-pad-list');
      } else {
        this.signalRService.joinCodePad(this.padId, user);
      }
    });
  }

  log(event, txt) {
    console.clear();
    console.log(txt);
    console.log(event);
  }

  codeChanged(event) {
    if (this.skipChange) {
      this.skipChange = false;
      return;
    }
    if (this.signalRService.isConnected) {
      this.signalRService.updateCodePad(this.padId, this.codePad);
    }
    this.http.put<void>(`SharedPad/${this.padId}`, this.codePad).subscribe(() => { });
  }

  run(): void {
    this.isRunning = true;
    this.http.post<RunResult>('SharedPad/Run', this.codePad).subscribe(result => {
      this.isRunning = false;
      this.runResult = result;
    });

  }
}
