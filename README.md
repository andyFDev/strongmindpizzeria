# StrongMindPizzeria

Backend: .Net Core Restful API. Clean Architecture with command/query repo.

Frontend: React Web App. Major Node packages: `Axio` for API Client, `TanStack Query` for queries, and `Material UI` for components (Not CSS files since styled tags are a good enough for small projects)

## 1.Building and running

The easiest way is to install [Visual Studio] (https://visualstudio.microsoft.com/) and follow the next steps:

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Open file `strongmindpizzeria.sln` with Visual Studio
4. Right click the solution icon -> `Build Solution`
5. Right click the solution icon -> `Properties` -> `Multiple statup projects` and Select `Start` on both WebApi and webui.
6. Click on `Debug` -> `Start Debbugin`

## 2. How to run tests

1. (If this point means the tests in code): Click `Test` -> `Test Explorer` -> `Run All Test in View`
2. (if this point means how to setup the page to test it): [Screen recording] (https://www.loom.com/share/f7444998310c4aa3ada701ac772fd7a2) using Loom with instructions on how to operate the page.
