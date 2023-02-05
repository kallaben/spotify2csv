import "reflect-metadata";
import express from "express";
import bodyParser from "body-parser";
const app = express();
const port = 3000;
import index from "./routes/index";
import session from "./middleware/session";
import cookieParser from "cookie-parser";
import setupDIContainer from "./middleware/setupContainer";
import { container } from "tsyringe";

app.use(cookieParser());
app.use(bodyParser.json());
app.use(setupDIContainer(container));
app.use(session(container));
app.use(index);

app.listen(port, () => {
  console.log(`App listening on port ${port}`);
});
