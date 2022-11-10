# Handling cursor based paging in MudBlazor using StrawberryShake and Mongo

## Nuget
| Package                                                                  | dotnet cli                                                                           |
|--------------------------------------------------------------------------|--------------------------------------------------------------------------------------|
| [HotChocolate.AspNetCore](https://github.com/chillicream/hotchocolate)   | `dotnet add package HotChocolate.AspNetCore`                                         |
| [HotChocolate.Data](https://github.com/chillicream/hotchocolate)         | `dotnet add package HotChocolate.Data`                                               |
| [HotChocolate.Data.MongoDb](https://github.com/chillicream/hotchocolate) | `dotnet add package HotChocolate.Data.MongoDb`                                       |
| [StrawberryShake](https://chillicream.com/docs/strawberryshake)          | `dotnet add package StrawberryShake.Blazor --version 13.0.0-preview.75 --prerelease` |
| [MongoDb](https://github.com/mongodb/mongo-csharp-driver)                | `dotnet add package MongoDB.Driver`                                                  |
| [Bogus](https://github.com/bchavez/Bogus)                                | `dotnet add package Bogus`                                                           |
| [MudBlazor](https://www.mudblazor.com)                                   | `dotnet add package MudBlazor`                                                       |

## Description
In the MudBlazor Frontend Web Application, I'm trying to wire up making a `MudTable` work with cursor based paging and GraphQl using StrawberryShake.
I'm new to using cursor based paging, and normally used offset based in the past.
From the way I implemented this, it seems rather complex to make it work.  Any feedback on how I did this is welcome.

I think everything works in the table, sorting, filtering, paging, but if you see any issues or anything I can do better, please let me know.

Please consider giving this a star if it was helpful to you.

Thanks,
Chris
