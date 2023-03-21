namespace Plants

open Microsoft.FSharp.Core

[<RequireQualifiedAccess>]
module Database =
    open Dapper
    open System.Collections.Generic
    open System.Data.Common

    let execute (connection: #DbConnection) (sql: string) (parameters: _) =
        try
            let result = connection.Execute(sql, parameters)
            Ok result
        with ex ->
            Error ex

    let query
        (connection: #DbConnection)
        (sql: string)
        (parameters: IDictionary<string, obj> option)
        : Result<seq<'T>, exn> =
        try
            let result =
                match parameters with
                | Some p -> connection.Query<'T>(sql, p)
                | None -> connection.Query<'T>(sql)

            Ok result
        with ex ->
            Error ex

    let querySingle (connection: #DbConnection) (sql: string) (parameters: IDictionary<string, obj> option) =
        try
            let result =
                match parameters with
                | Some p -> connection.QuerySingleOrDefault<'T>(sql, p)
                | None -> connection.QuerySingleOrDefault<'T>(sql)

            if isNull (box result) then Ok None else Ok(Some result)
        with ex ->
            Error ex