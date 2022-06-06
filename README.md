# Crapper
A *not* useful twitter implementation in C#.

## How to build project
Modify ConnectionStrings and Jwt in ```appsettings.[Environment].json```: 
 ```json
 "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Audience": "YOUR_HOST",
    "Issuer": "YOUR_HOST"
  }
 ```
 keep in mind that default dev host is [```http://localhost:5298```].

 run migrations by:
 ```
 dotnet ef database update
 ```

 ## About project 
 This repo is just presentation of how I see product development and technologies I know. In many problems and their solutions I was not sure about accuracy due to lack of practice. 
 
## Code structure
coming soon...
