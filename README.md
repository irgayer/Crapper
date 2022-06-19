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
 
# Top footguns in this project
1) Generic repository - not every model can be deleted/updated. After careful consideration, I realized that every route requires different data and I had no choice but to include additional models [(example)](https://github.com/irgayer/Crapper/blob/main/Crapper/DAL/EF/Repositories/UserRepository.cs). At this point I didn't figure out that I can add extra abstraction layer implementing queries.
2) Filters was hardcoded [example#1](https://github.com/irgayer/Crapper/blob/main/Crapper/Filters/ValidateEntityExists.cs) and [example#2](https://github.com/irgayer/Crapper/blob/main/Crapper/Filters/UserPostAccessFilter.cs).
3) The most painful thing is that I didn't know how to test the application well and each change led to 2-3 hours of debugging. Yes, now I know about TDD. 
4) Project separation - It was 100% monolith that caused 2-3 min build time and I was very concerned about it. The project doesn't even have 1000 lines of code and it is already slow. 

