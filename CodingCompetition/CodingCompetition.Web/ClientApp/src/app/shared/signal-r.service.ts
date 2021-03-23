import { Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';
import { CodePad } from '../models/shared-pad/code-pad.model';
import { Message } from '../models/message.model';
import { CodePadUser } from '../models/shared-pad/code-pad-user.model';


@Injectable({ providedIn: 'root' })
export class SignalRService {

  messageReceived$ = new Subject<CodePad>();
  userJoined$ = new Subject<CodePadUser>();
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
    this.hubConnection.invoke('UpdatePad', id, new Message<CodePad>(this.hubConnection.connectionId, codePad));
  }

  joinCodePad(id: string, userName: string) {
    this.hubConnection.invoke('JoinGroup', id, userName);
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
    this.hubConnection.on('Update', (message: Message<CodePad>) => {
      if (message.sender !== this.hubConnection.connectionId) {
        this.messageReceived$.next(message.data);
      }
    });

    this.hubConnection.on('Join', (message: CodePadUser) => {
      console.log(message);
      this.userJoined$.next(message);
    });

    this.hubConnection.on('Leave', (message: string) => {
      console.log(message);
      this.userLeft$.next(message);
    });

    this.hubConnection.on('Remove', (message: string) => {
      console.log(message);
      this.codePadRemoved$.next(message);
    });
  }
}
