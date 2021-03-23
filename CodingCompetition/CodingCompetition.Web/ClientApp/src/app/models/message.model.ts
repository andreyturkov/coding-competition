export class Message<T> {
  sent: string;
  sender: string;
  data: T;

  constructor(sender: string, data: T) {
    this.sent = new Date().toISOString();
    this.sender = sender;
    this.data = data;
  }
}
