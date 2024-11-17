# SeniorConnect

## About

### The why
This project was created as an educational exercise for my Second semester on Fontys-Eindhoven

### The what
Senior connect is a web application designed to create a community of elderly people in order to minimize loneliness amongst them.<br>
this Project specifically focuses on Group chats and forms where these elderly people can communicate with eachother and ask questions.<br>
It will eventually be part of a larger system and work together with three other applications that work towards the same cause.

## How to run

### Building the application for production
Building the application can be done using the following command:
```
dotnet publish -c Release -o ./publish
```

this will create a folder called ```publish``` which is where you'l find the compiled project.

### Building the application for development
To run the application for development simply use the command ``` dotnet run ``` and the application will start.

## Useful commands
To make my life easier I created a few custom commands that can be executed in order to run some tasks.

### migration command
The migration command will run the configured migration on the database. <br>
It wil remove all tables from the database, and create them again. <br>
You can see it as a simple way to clean the database and bring it back to it's empty state.
```
dotnet run migration
```

Additionally, you can add ``` seed ``` to the end of the command to also run the seeders.<br>
Seeders are a series of insert statements that will populate the database with standard data like user roles and such.<br>
Note that the seeder does not generate test data.
``` 
dotnet run migration seed 
```
### factory command
Factorys are a way of inserting data into the database for testing purposes.<br>
It will populate the database with random (but realistic) data for testing.
#### disclaimer: not yet implemented.
```
dotner run factory
```

### test command
command that runs a function that can contain temporary code for testing purposes.
```
dotnet run test
```
