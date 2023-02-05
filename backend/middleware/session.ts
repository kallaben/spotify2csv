import { DependencyContainer } from "tsyringe";
import express, { Request, Response, NextFunction, Express } from "express";
import { v4 } from "uuid";
import UserContext from "../services/userContext";

function setup(container: DependencyContainer): Express {
  const app = express();

  app.use((req: Request, res: Response, next: NextFunction) => {
    const sessionId = req.cookies.sessionId ?? v4();

    container.register<UserContext>(UserContext, {
      useValue: new UserContext(sessionId),
    });

    res.cookie("sessionId", sessionId);

    next();
  });

  return app;
}

export default setup;
