import { Request } from "express";

export default class RequestProvider {
  constructor(private req: Request) {}

  getRequest(): Request {
    return this.req;
  }
}
