namespace Plants

open Dapper
open Microsoft.FSharp.Core
open Npgsql

[<RequireQualifiedAccess>]
module Data =

    open FSharp.Configuration

    type Config = YamlConfig<"appsettings.yaml">
    let config = Config()
    let cs = config.db.connection_strings.postgres

    let private connection (connectionString: string) =
        let c = new NpgsqlConnection(connectionString)
        c.Open()
        c

    let all () =
        let db = connection cs
        let sql = "select * from plant"

        Database.query db sql None

    let insert plant =
        let db = connection cs
        let parameters = DynamicParameters(plant)

        let sql =
            "insert into plant
                  (id, name, location, date_planted,
                  is_alive, owner, owner_email)
                  values
                  (@id, @name, @location, @datePlanted,
                  @isAlive, @owner, @ownerEmail)"

        Database.execute db sql parameters