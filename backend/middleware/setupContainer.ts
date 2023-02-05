import RequestProvider from "../services/requestProvider";

import express, { Express } from "express";
import { Request, Response, NextFunction } from "express";
import { DependencyContainer } from "tsyringe";

function setup(container: DependencyContainer): Express {
  const app = express();

  app.use((req: Request, res: Response, next: NextFunction) => {
    container.register<RequestProvider>(RequestProvider, {
      useValue: new RequestProvider(req),
    });

    next();
  });

  return app;
}

export default setup;
