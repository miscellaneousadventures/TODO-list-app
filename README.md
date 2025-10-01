BACKEND:
cd TODO-list-app\backend\TodoListApi
dotnet restore
dotnet run
runs on http://localhost:5159

backend runs on http://localhost:5159 no HTTPS setup is required.

FRONTEND:
cd TODO-list-app\frontend\simple-todo-list
npm install
ng serve
runs on http://localhost:4200

OpenAPI doco:
http://localhost:5159/openapi/ToDoList.json

Basic test:
cd TODO-list-app\frontend\simple-todo-list
ng test

