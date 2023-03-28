docker run -e POSTGRES_PASSWORD=adminadmin -e POSTGRES_DB=contacts -p 5432:5432 -d postgres:latest
cd ContactsApp
dotnet ef database update
cd client
ng serve --host=127.0.0.1 &
cd ..
dotnet run -lp https

