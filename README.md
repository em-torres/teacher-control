# teacher-control - WIP

| Dependencies
| ------------------------------------------------------------------------- |
| [.NET Core](docs.microsoft.com/en-us/dotnet/core/#download-net-core-21)   |
| [EF Core](https://docs.microsoft.com/en-us/ef/core)                       |


# TODO: Road to 1.0v release
- [ ] Review db validations and default values on the EFCore library
- [ ] Use the services to inject the model db validation and other classes that have a base class or an Interface
- [ ] Adding Comments
- [ ] Improve code accessability
- [ ] Adding Redis cache svc
- [ ] Adding prod, dev configuration, variables
- [ ] Structure the root folder with the config deployment files, src(where the apps and service goes), build(pre-build-conf and on-build-conf)
- [ ] Docker support
- [ ] CircleCi support
- [ ] Add 404 route service
- [ ] Add identity service
- [ ] Seed Db from the Enums in the Migration svc
- [ ] Add ILogger service on: application layer, repos, startup, request/response middleware, on-config services
- [ ] Create README.md with the the desciption fo the software
- [ ] add HATEOS on the app layer
- [ ] Add Docker support for the infraestructure dependencies
- [ ] Add audit interfaces to the entities that may need those field, put all that on the DbCOntext;s saveCHanges method
- [ ] Add soft delete field on the entities
- [ ] Add filters for the queries on the get methods to force the users to use snake case on the params
- [ ] Adding more supportedCultures Support 
- [ ] Add jobs like: email, notifications, commitment checker, etc
- [ ] Move Entities, DTOs and Domain Enums on a new Class library 'Core'
- [ ] Adding Upvotes that store who is giving and when
- [ ] Addinng BLog module per courses
- [ ] Update the saveChanges usage on the repositories
- [ ] Configure EF core to run in memory when the environment flag i settled to TEST | DEV
- [ ] Deploy :tada::metal: