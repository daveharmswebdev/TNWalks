```text
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sql mcr.microsoft.com/azure-sql-edge
```



docker run -it \
-e "ACCEPT_EULA=Y" \
-e "SA_PASSWORD=A&VeryComplex123Password" \
-p 1433:1433 \
--platform linux/amd64 \
--name sql-server-sucks \
mcr.microsoft.com/mssql/server:2022-latest
