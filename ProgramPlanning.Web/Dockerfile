FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ProgramPlanning.Web/ProgramPlanning.Web.csproj", "ProgramPlanning.Web/"]
RUN dotnet restore "ProgramPlanning.Web/ProgramPlanning.Web.csproj"
COPY . .
WORKDIR "/src/ProgramPlanning.Web"
RUN dotnet build "ProgramPlanning.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProgramPlanning.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProgramPlanning.Web.dll"]