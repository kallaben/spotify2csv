import express, { Request, Response } from "express";
import querystring from "querystring";
const router = express.Router();
const client_id = "543a4066a8a94ff7ab4705453913eb4e";
import { container } from "tsyringe";
import UserContext from "../services/userContext";

router.get("/callback", (req: Request, res: Response) => {
  console.log(
    `Callback called with the following parameters: ${JSON.stringify(
      req.query
    )}`
  );

  const sessionId = container.resolve(UserContext).getId();
  console.log({ sessionId });

  res.status(200).send();
});

// app.get("/login", (req: Request, res: Response) => {
//   res.redirect(
//     "https://accounts.spotify.com/authorize?" +
//       querystring.stringify({
//         response_type: "code",
//         client_id: client_id,
//         scope: "playlist-read-private",
//         redirect_uri: "http://localhost:4200/api/callback",
//         state: state,
//       })
//   );
// });

export default router;
