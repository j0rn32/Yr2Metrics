# Yr2Metrics
.Net6 minimal api

Retrieves weather data from Yr.no using the NowCast 2.0 API.
Prometheus metrics will be available on http://localhost/metrics

## Running
Config from appsettings.json:
```dotnet run```

Config from parameters:
```dotnet run -- --Yr:UserAgent="testapp nameATdomain.com" --Yr:Latitude=59.13 --Yr:Longitude=10.16```

## Docker
Build:
```docker build -t yr2metrics .```

Run with parameters from appsettings.json interactive:
```docker run --rm -t -p 80:80 yr2metrics .```

Run with parameters interactive:
```docker run -i --rm -p 80:80 -e "Yr:UserAgent"="me meATmail.com" -e "Yr:Latitude"=99 -e "Yr:Longitude"=33 -t yr2metrics```

Docker run with parameters:
```docker run -d --restart unless-stopped -p 80:80 -e "Yr:UserAgent"="me meATmail.com" -e "Yr:Latitude"=99 -e "Yr:Longitude"=33 -t yr2metrics```
