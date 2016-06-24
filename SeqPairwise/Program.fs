open System
open System.IO

let backpackTrail = [| 552;398;402;399;481;512;392;350|] 
let totalUphillClimb heights =
    heights
    |> Seq.pairwise
    |> Seq.filter(fun(x, y) -> y > x)
    |>Seq.map(fun(x, y) -> y - x)
    |> Seq.sum
totalUphillClimb backpackTrail |> printfn "Total uphill hike in feet: %i"

let peak heights =
    heights
    |> Seq.windowed 3 
    |> Seq.choose(fun triplet -> 
        match triplet with 
        | [|a;b;c|] when b > a && b > c -> Some b 
        | _ -> None)
peak backpackTrail |> printfn " We've got peaks at: %A"

[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    0 // return an integer exit code

