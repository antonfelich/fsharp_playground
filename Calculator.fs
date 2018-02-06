module Calculator

type Parcel = {
    weight: float
    width: int
    height: int
    depth: int
}

type CalculatorResult = Result<decimal, string>
type Rule = Parcel -> CalculatorResult option

let calculate (rules: Rule seq) (parcel: Parcel): string =

    let picker = 
        (fun rule -> 
            parcel 
            |> rule
        )   
    
    match rules |> Seq.tryPick picker with
    | Some (Ok value) ->
        value |> string
    | Some (Error error) ->
        error |> sprintf "Error: %s"
    | None ->
        "no matching rules"