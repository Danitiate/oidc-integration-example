# Use an official Node.js runtime as the base image
FROM node:18-alpine AS build

# Set the working directory inside the container
WORKDIR /Frontend

# Copy package.json and package-lock.json to the container
COPY /Frontend/package.json .
COPY /Frontend/package-lock.json .

# Install dependencies
RUN npm install

# Copy the rest of the application's source code to the container
COPY /Frontend/. /Frontend/

# Build the Angular app for production
RUN npm run build -- --c production

# Use web server to serve the Angular app
FROM nginx:alpine
COPY /Frontend/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /Frontend/dist/oidc-example-app /usr/share/nginx/html
EXPOSE 8080

CMD ["nginx", "-g", "daemon off;"]