FROM node:16-alpine as node
WORKDIR /app
COPY . .
RUN npm i -g npm@8.5.0
RUN npm install

ARG ENV=production
RUN npm run build:${ENV}

FROM nginx:1.19-alpine
COPY ./nginx.conf /etc/nginx/nginx.conf
COPY --from=node /app/dist/app /usr/share/nginx/html
