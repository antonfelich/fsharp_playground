module Calculator

type Parcel = {
    weight: decimal
    width: decimal
    height: decimal
    depth: decimal
}

type CalculatorResult = Result<decimal, string>
type Rule = Parcel -> CalculatorResult option

let calculate (rules: Rule seq) (parcel: Parcel): CalculatorResult option =

    let picker = 
        (fun rule -> 
            parcel 
            |> rule
        )   
    
    rules |> Seq.tryPick picker

let volume (parcel: Parcel): decimal = 
    parcel.depth * parcel.width * parcel.height