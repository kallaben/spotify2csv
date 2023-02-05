export default class UserContext {
  constructor(private userId: string) {}

  public getId(): string {
    return this.userId;
  }
}
