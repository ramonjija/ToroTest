# 💸 Toro Application - Investing Toro 📈

This Application is used to buy shares for authenticated users of Toro Investments.
The application provides the creation of the user (Sign Up), as well as login (Sign In).
From there the authenticated user can buy shares that are pre-loaded at the Database if the user has the necessary amout of Balance.
The Application also provides a way to Add Balance to the user, make it possible to buy shares.

## Demo

![Demo Toro Investing](https://i.imgur.com/0q6yVUH.gif)


## Instalation

Use [docker-compose](https://docs.docker.com/compose/install/) to run all the required containers.

```
docker-compose up -d
```

*Obs: If you are using Docker in MacOS add the following paths to the File Sharing settings.json at docker:

  ~/Library/Group\ Containers/group.com.docker/settings.json

  ```
  ~/Library/Group\ Containers/group.com.docker/settings.json
  ```
  Add at field filesharingDirectories:

  ```
  “filesharingDirectories” : [
    “\/Users”,
    “\/Volumes”,
    “\/private”,
    “\/tmp”
  ],
  ```
  The values:
  ```
  “\/Microsoft\/UserSecrets”
  “\/ASP.NET\/Https”
  ```

  Resulting in:

  ```
  “filesharingDirectories” : [
    “\/Users”,
    “\/Volumes”,
    “\/private”,
    “\/tmp”,
    “\/Microsoft\/UserSecrets”,
    “\/ASP.NET\/Https”
  ],
  ```

The command should be runned at root folder of the application, where <b>docker-compose.yml</b> is located.


## Usage

After all the containers are up, navigate to http://localhost:3000 to go the Toro's Investments Sign In page.

From there, create your User with a valid CPF and you'll be redirected to the main page of the Application, the User Position.

You can Add "money" to your balance to buy some Shares.

The API can be access through the address http://localhost:5100, and uses JWT for Authentication and Authorizarion of some resources such as:  

* Sign In the application;
* Get the User Position;
* Add Balance to the User Position;
* Buy Shares;

The application is based on the following principles:  

* User must be logged access the resources of the application;
* Logged user can View the Balance of the account;
* Logged user can View the Consolidated Balance of the account, that is the sum of all the positions plus the Current Balance;
* Logged user can Add money to your Balance to buy Shares;
* Logged user can Buy Shares only if the user has the required amount of Balance in account;

## Tools

The Application is built on:
* Frontend:
  * React
  * React-Material-UI

* Backend:
  * .Net Core 3.1
  * Sql Server 2017
  * Cryptography (BCrypt.Net)
  * JWT Token
  * EF Core
  * NUnit

* Infrastructure
  * Docker
  * Docker-Compose
