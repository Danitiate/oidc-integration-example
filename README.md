# OpenIdConnectIntegrationExample

OpenIdConnectIntegrationExample is a sample application for developers who wants to get started configuring Open Id Connect for enterprises.

- The project uses Angular in the front end and is used for configuring an integration with a OIDC provider.
- The backend uses .NET C# and uses a REST API to handle all logic
- Configured using code first Entity Framework and SQLite as a database
- Fully setup using Docker

## Usage

This is optional, but as the program uses containers and SQLite, you will need to create a Docker volume where the data can be stored. 
If not, all data will be lost when creating a new container.
```bash
docker volume create sample-data
```

Use docker for setting up and building the environment

```bash
docker-compose up --build
```

This will create two containers.
- Front end: [http://localhost:8080](http://localhost:8080) 
- Back end: [http://localhost:8081](http://localhost:8081)

You can also run the front end locally using:
```bash
ng serve
```
Which will be useful for developing with live code changes.


## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.