namespace Plants

open System
open Dapper

module Wrappers =
    let isNullOrEmpty (s: string) = String.IsNullOrEmpty(s)
    let today = DateOnly.FromDateTime(DateTime.Now)
    let setTextColor (color:ConsoleColor) = Console.ForegroundColor <- color
    let resetColor() = Console.ResetColor()
    let readLine() = Console.ReadLine()
    let readKey() =
        let k = Console.ReadKey()
        Console.WriteLine()
        k.KeyChar |> string
    let dapperUseSnakeCase = DefaultTypeMap.MatchNamesWithUnderscores <- true