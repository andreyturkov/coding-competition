import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';

import { ApiService } from '../shared/api.service';
import { SignalRService } from '../shared/signal-r.service';
import { CodePad } from '../models/code-pad.model';

@Component({
  selector: 'shared-pad',
  templateUrl: './shared-pad.component.html',
  styleUrls: ['./shared-pad.component.css']
})
export class SharedPadComponent implements OnInit, OnDestroy {

  sharedPadId: string;
  codePad: CodePad;
  connectionEstablished: Observable<boolean>;
  options: any = { printMargin: false, enableBasicAutocompletion: true };

  constructor(private http: ApiService,
    private route: ActivatedRoute,
    private router: Router,
    private signalRService: SignalRService) {
    this.connectionEstablished = this.signalRService.connectionEstablished$;
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.sharedPadId = params['id'];

      this.http.get<CodePad>(`SharedPad/${this.sharedPadId}`).subscribe(result => {
        this.codePad = result;
      });

      this.connectionEstablished.subscribe((result) => {
        if (result) {
          this.signalRService.joinCodePad(this.sharedPadId);
        }
      });
    });
    this.signalRService.messageReceived$.subscribe((message) => {
      this.codePad = message;
    });
    this.signalRService.codePadRemoved$.subscribe((padId) => {
      if (this.sharedPadId === padId) {
        this.router.navigateByUrl('/shared-pad-list');
      }
    });
  }

  ngOnDestroy(): void {
    if (this.signalRService.isConnected) {
      this.signalRService.leaveCodePad(this.sharedPadId);
    }
  }

  log(event, txt) {
    console.clear();
    console.log(txt);
    console.log(event);
  }

  codeChanged(event) {
    if (this.signalRService.isConnected) {
      this.signalRService.updateCodePad(this.sharedPadId, this.codePad);
    }
    this.http.put<void>(`SharedPad/${this.sharedPadId}`, this.codePad).subscribe(() => { });
  }
}
