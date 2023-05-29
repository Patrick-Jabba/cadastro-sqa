# estágio de compilação
FROM node:lts-alpine AS build-stage

WORKDIR /app

COPY fe-cadastroSqa/package*.json .
RUN npm install
COPY fe-cadastroSqa .
RUN npm run build

# estágio de produçao
FROM nginx:stable-alpine AS production-stage

COPY --from=build-stage /app/dist/ /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

