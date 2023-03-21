namespace Plants

open System
open Wrappers

[<CLIMutable>]
type Plant =
    { id: Guid
      name: string
      location: string
      datePlanted: DateTime
      isAlive: bool
      owner: string
      ownerEmail: string }

[<RequireQualifiedAccess>]
module Plant =
    let private isValidOwnerEmail (email: string) =
        try
            Net.Mail.MailAddress(email) |> ignore
            true
        with _ ->
            false

    let validate plant =
        let errors =
            seq {
                if (isNullOrEmpty plant.ownerEmail) then
                    yield "Owner email should not be empty"

                if (isNullOrEmpty plant.name) then
                    yield "Plant name should not be empty"

                if (isNullOrEmpty plant.location) then
                    yield "Your plant should have a location"

                if (isNullOrEmpty plant.owner) then
                    yield "Your plant should have an owner"

                if (isNullOrEmpty plant.ownerEmail) then
                    yield "Owner email should not be empty"

                if (isValidOwnerEmail plant.ownerEmail |> not) then
                    yield "Owner email is invalid"
            }

        if (Seq.isEmpty errors) then Ok plant else Error errors

    let createPlant name location owner ownerEmail =
        let p =
            { id = Guid.NewGuid()
              name = name
              location = location
              datePlanted = DateTime.Today
              isAlive = true
              owner = owner
              ownerEmail = ownerEmail }

        validate p