# Sql Server container can take some time to start up. 
# This may need to be tweaked per machine, e.g. I can set this to 10s and it's fine
sleep 30s

# Note the SA password here as well.
# Ideally, I'd have an environment variable to handle this (maybe we can use the SA_PASSWORD one?)
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Pa55w0rd<db0562096> -d master -i /app/InitialSQL.sql