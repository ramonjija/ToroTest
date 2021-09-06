# 💸 Toro Application - Investing Toro 📈

This Application is used to buy shares for authenticated users of Toro Investments. 
The application provides the creation of the user (Sign Up), as well as login (Sign In).
From there the authenticated user can buy shares that are pre-loaded at the Database if the user has the necessary amout of Balance.
The Application also provides a way to Add Balance to the user, make it possible to buy shares.

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

The API uses JWT for Authentication and Authorizarion of some resources such as:  

  1 - Sign In to the application;
  2 - Get the User Position;
  3 - Add Balance to the User Position;
  4 - Buy Shares;

The application is based on the following principles:  

- User must be logged to get access of the application
- Logged user can view the Balance of the account;
- Logged user can view the Consolidated Balance of the account, that is the sum of all the positions plus the Current Balance;
- Logged user Add Balance to buy Shares;
- Logged user can buy Shares only if the user has the required amount of Balance in account;

## Tools

The Application is built on:

-Frontend:
-- React
-- React-Material-UI

-Backend:
-- .Net Core 3.1
-- Sql Server 2017
-- Cryptography (BCrypt.Net)
-- JWT Token
-- EF Core
-- NUnit

-Infrastructure
-- Docker
-- Docker-Compose
