# Push Notification Service

## To Run:

```
docker-compose up --build
```

**The Web API is going to run on 1234 port.**

## API Overview:

**GET /api/users/{username} → Get user details.**

- 200 OK: user info
- 404 Not Found: user not found

**POST /api/users/register → Register new user.**
Body: { username, password, deviceToken }

- 200 OK: user ID
- 400 Bad Request: the username has been taken
- 400 Bad Request: you sent invalid data

**GET /api/notifications/history/{username}?limit=&fromUtc=&toUtc= → Get user’s notification history.**

- 200 OK: list of notifications
- 404 Not Found: user not found

**POST /api/notifications/send → Send notification.**

Body: { username, title, text }

- 200 OK: notification ID
- 400 Bad Request: you sent invalid data
- 404 Not Found: user not found