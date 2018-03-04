open Calculator


[<EntryPoint>]
let main args =

    let parcel = 
        {
            width = 10M
            height = 10M
            depth = 10M
            weight = 10M
        }

    let tooHeavy 
        (parcel: Parcel)
        : CalculatorResult option =
        None

    let heavyParcel
        (parcel: Parcel)
        : CalculatorResult option =
        if parcel.weight > 10M
        then 
            15M * parcel.weight 
            |> Ok
            |> Some
        else 
            None

    let smallParcel
        (parcel: Parcel)
        : CalculatorResult option =
        let parcelVolume = parcel |> volume
        if parcelVolume > 1500M
        then 
            0.05M * parcelVolume
            |> Ok
            |> Some
        else 
            None

    let mediumParcel
        (parcel: Parcel)
        : CalculatorResult option =
        let parcelVolume = parcel |> volume
        if parcelVolume > 2500M
        then 
            0.04M * parcelVolume
            |> Ok
            |> Some
        else 
            None

    let largeParcel
        (parcel: Parcel)
        : CalculatorResult option =
        let parcelVolume = parcel |> volume        
        0.03M * parcelVolume
            |> Ok
            |> Some

    let rules = 
        [
            tooHeavy
            heavyParcel
            smallParcel
            mediumParcel
            largeParcel
        ]

    
    match parcel |> calculate rules with
    | Some (Ok value) ->
        value |> string
    | Some (Error error) ->
        error |> sprintf "Error: %s"
    | None ->
        "no matching rules"
    |> System.Console.WriteLine

    0