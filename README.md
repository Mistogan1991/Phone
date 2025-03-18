# Phone

## Developing the Http server
For developing HTTP server i used C# and .Net 7 

## Dockerization
Here is the Dockerfile that i create for application.
this file must be one folder befor your projec.

```yaml
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Phone.Api/Phone.Api.csproj", "Phone.Api/"]
RUN dotnet restore "./Phone.Api/Phone.Api.csproj"
COPY . .
WORKDIR "/src/Phone.Api"
RUN dotnet build "./Phone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Phone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phone.Api.dll"]
```

## Deployment
For deployment on preinstalled Docker Ubuntu server i used from WSL(Windows Subsystem for Linux)

[How to install Linux on Windows with WSL](https://learn.microsoft.com/en-us/windows/wsl/install) - 
[Install Ubuntu on WSL2](https://documentation.ubuntu.com/wsl/en/latest/howto/install-ubuntu-wsl2/)

after that you need instal docker on you ubuntu server - [Install Docker Engine on Ubuntu](https://docs.docker.com/engine/install/ubuntu/)
(In this part if you, like me, don't know Linux, you will have a lot of troubles- I've searched a lot and have used from chat gpt)

after you install docker correctly transter your project to ubuntu server
I've used [WinSCP](https://winscp.net/eng/index.php) to transfer my files

In directory of your Project that you DockerFile is located run this commands

```
docker build -t mafioso .
docker run -d -p 8080:8080 --name mafioso-api mafioso
```
### First Command: Build the Docker Image
🔹 What It Does:

`docker build` → Builds a Docker image from the Dockerfile in the current directory (.).

`-t mafioso` → Tags the image with the name mafioso (so you can refer to it later).

After running this command, you’ll have a Docker image named mafioso stored locally.

### Second Command: Run the Container
🔹 What It Does:

`docker run` → Starts a new container from an image.

`-d` → Runs the container in the background (detached mode).

`-p 8080:8080` → Maps port 8080 on your host machine to port 8080 inside the container.

`--name mafioso-api` → Assigns a friendly name (mafioso-api) to the container.

`mafioso` → Uses the mafioso image (built earlier).

Your container is now running in the background.
You can access your application at your browser:
```
http://<your-wsl-ip>:8080/swagger/index.html
```
 
### Check If Your Container Is Running

Run:

```bash
docker ps
```
You should see something like:

```nginx
CONTAINER ID   IMAGE     COMMAND                  STATUS         PORTS                    NAMES
123abc456def   mafioso   "dotnet Mafioso.dll"     Up 10 minutes  0.0.0.0:8080->8080/tcp   mafioso-api
```
If you need to stop or remove the container:

```bash
docker stop mafioso-api
docker rm mafioso-api
```









