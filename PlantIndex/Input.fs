namespace Plants

[<RequireQualifiedAccess>]
module Input =
    open System
    open Wrappers

    let private captureInput (label: string) =
        printf $"{label}"
        readLine ()

    let private printErrors errors =
        setTextColor ConsoleColor.Red
        printfn "ERRORS"
        resetColor ()
        errors |> Seq.iter (printfn "%s")

    let rec private parsePlant () =
        printfn "PARSING PLANT"

        Plant.createPlant
            (captureInput "Name: ")
            (captureInput "Location: ")
            (captureInput "Owner: ")
            (captureInput "Owner email: ")
        |> fun r ->
            match r with
            | Ok plant -> plant
            | Error err ->
                printErrors err
                parsePlant ()

    let private captureChoice savePlant =
        let plant = parsePlant ()
        savePlant plant
        let another = captureInput "Continue? Y/n"

        match another with
        | "Y" -> Choice1Of2()
        | _ -> Choice2Of2()

    let rec private parsePlants savePlant =
        match captureChoice savePlant with
        | Choice1Of2 _ -> parsePlants savePlant
        | Choice2Of2 _ -> ()

    let printMenu () =
        setTextColor ConsoleColor.Yellow
        printfn "============"
        printfn "MENU"
        printfn "============"
        resetColor ()
        printfn "1. Show plants"
        printfn "2. Add a plant"
        printfn "0. Quit"

    let routeMenuOption i getPlants savePlant =
        match i with
        | "1" ->
            printfn "Plants"

            getPlants ()
            |> List.iter (fun p ->
                let isAlive =
                    match p.isAlive with
                    | true -> "alive"
                    | false -> "deceased"

                printfn
                    $"{p.name} | {p.location} \
                    | {p.datePlanted.ToShortDateString()} \
                    | {p.owner} | {isAlive}")
        | "2" -> parsePlants savePlant
        | _ -> printMenu ()