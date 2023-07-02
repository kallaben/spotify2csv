# Step-by-step setup
## Frontend:
1. Navigate to the frontend folder.
2. Install the node version specified in the `.nvmrc` file (you can use [nvm-windows](https://github.com/coreybutler/nvm-windows) to install Node on Windows).
3. Run `npm install`.
4. Run `npm start`.

The frontend should be up on running now.

## Backend:
1. Navigate to the `dotnet_backend/api` folder.
2. Run `docker compose up` to start the Mongo database.
3. Go to the [Spotify for developers dashboard](https://developer.spotify.com/dashboard/543a4066a8a94ff7ab4705453913eb4e/settings) to get the client secret used to communicate with the Spotify API. The secret needs to be inserted in `dotnet_backend/api/appsettings.Development.json`.
3. Open the `dotnet_backend.sln` file in rider (located in the `dotnet_backend` folder).
4. Run the "api" launch configuration through Rider (you will be informed if you are missing the correct dotnet SDK, and will get a link to install it in the console output).

Once the backend and frontend are both running, you should be able to access the app at the http://localhost:4200/ domain.
