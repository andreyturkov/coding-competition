import { Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';

import { CodePad } from '../models/code-pad.model';

@Injectable({ providedIn: 'root' })
export class SignalRService {

  messageReceived$ = new Subject<CodePad>();
  userJoined$ = new Subject<string>();
  userLeft$ = new Subject<string>();
  codePadRemoved$ = new Subject<string>();
  connectionEstablished$ = new BehaviorSubject<boolean>(false);

  get isConnected(): boolean {
    return this.hubConnection.state === HubConnectionState.Connected;
  }

  private hubConnection: HubConnection;

  constructor(@Inject('BASE_URL') private baseUrl: string) {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  updateCodePad(id: string, codePad: CodePad) {
    this.hubConnection.invoke('SendCodePad', id, codePad);
  }

  joinCodePad(id: string) {
    this.hubConnection.invoke('JoinGroup', id);
  }

  leaveCodePad(id: string) {
    this.hubConnection.invoke('LeaveGroup', id);
  }

  private createConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + 'SharedPadHub')
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();
  }

  private startConnection() {
    if (this.isConnected) {
      return;
    }

    this.hubConnection.start().then(() => {
      console.log('Hub connection started!');
      this.connectionEstablished$.next(true);
    }, error => console.error(error));
  }

  private registerOnServerEvents(): void {
    this.hubConnection.on('CodePad', (data: any) => {
      this.messageReceived$.next(data);
    });

    this.hubConnection.on('Join', (data: any) => {
      console.log(data);
      this.userJoined$.next(data);
    });

    this.hubConnection.on('Leave', (data: any) => {
      console.log(data);
      this.userLeft$.next(data);
    });

    this.hubConnection.on('RemoveCodePad', (data: any) => {
      console.log(data);
      this.codePadRemoved$.next(data);
    });
  }
}
