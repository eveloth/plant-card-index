open System
open Plants
open Wrappers

let getPlants () =
    Data.all ()
    |> fun r ->
        match r with
        | Ok plants -> plants |> Seq.toList
        | Error e ->
            setTextColor ConsoleColor.Red
            printfn $"ERROR: {e.Message}"
            resetColor ()
            List.empty

let addPlant plant =
    Data.insert plant
    |> fun r ->
        match r with
        | Ok i -> printfn $"{i} records inserted"
        | Error e ->
            setTextColor ConsoleColor.Red
            printfn $"ERROR: {e.Message}"
            resetColor ()

[<EntryPoint>]
let main argv =
    dapperUseSnakeCase
    Input.printMenu()
    let mutable selection = readKey()
    while (selection <> "0") do
        Input.routeMenuOption selection getPlants addPlant
        Input.printMenu()
        selection <- readKey()
    0