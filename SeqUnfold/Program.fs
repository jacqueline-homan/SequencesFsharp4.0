open System
open System.Numerics
open SeqUnfold.Seq

let numbers = seq {for i in 1..100 -> i}
let fizzbuzzUnfold() =
    1
    |> Seq.unfold (fun i ->
        if i >= 100 then None
        else
            let number = 
                if i % 3 = 0 && i % 5 = 0 then "FizzBuzz"
                elif i % 3 = 0 then "Fizz"
                elif i % 5 = 0 then "Buzz"
                else i.ToString()
            Some(number, i + 1)) 
    |> Seq.iter(fun s -> printfn "%s" s)
fizzbuzzUnfold()

(*We can get fancy by creating an inifinite sequence to define 
an IsMultipleOf function and then use that in lieu
of i % n = 0 for our Fibonnacci example: 

let isMultipleOf = Seq.initInfinite (fun x -> (x * 1) / x = 0)
*)


let test =
    1
    |> Seq.unfold(fun x -> 
        if x <= 5 then Some (sprintf "x: %i" x, x + 1)
        else None)
    |> Seq.iter(fun x -> printfn "%s" x) 


let isWorkDay(day:DateTime) =
    day.DayOfWeek <> DayOfWeek.Saturday && day.DayOfWeek <> DayOfWeek.Sunday

let daysFollowing(start:DateTime) =
    Seq.initInfinite(fun x -> start.AddDays(float(x)))
//daysFollowing(DateTime(2016, 6, 1)) |> printfn "%A"
//|> Seq.take 5
//|> Seq.iter(printfn "%A")

let workday start =
    start
    |> daysFollowing
    |> Seq.filter isWorkDay
    |> Seq.take 3
//    |> Seq.iter(printfn "%A")
printfn "%A" (workday(DateTime(2016, 6, 10)))

let nextWorkdayAfter interval start =
    start
    |> workday
    |> Seq.item interval
printfn "%A" (nextWorkdayAfter 2 (DateTime(2016, 6, 10)))

let actionDays startDate endDate interval =
    Seq.unfold(fun date -> 
        if date > endDate then None
        else 
            let next = date |> nextWorkdayAfter interval
            let dateString = date.ToString("dd-MMM-yy dddd")
            Some (dateString, next)) startDate 

//let result = actionDays (DateTime(2016, 6, 10)) (DateTime(2016, 7, 10)), 3 |> Array.toSeq
//printfn "%A" result
//printfn "%A"(actionDays (DateTime(2016, 6, 10)) (DateTime(2016, 7,1)) 3) 
//actionDays (DateTime(2016, 6, 10) (DateTime(2016, 7,1) 3) |> Seq.iter(printfn "%A")
let seqInfinite = Seq.initInfinite (fun index ->
    let n = float( index + 1 )
    1.0 / (n * n * (if ((index + 1) % 2 = 0) then 1.0 else -1.0)))
printfn "%A" seqInfinite

(*[3;3;2;4;1;2;8] 
|> Seq.mapi (fun i v -> i, v) // Add indices to the values (as first tuple element)
|> Seq.groupWhen (fun (i, v) -> i%2 = 0) // Start new group after every 2nd element
|> Seq.map (Seq.map snd) // Remove indices from the values
|> Seq.iter(printfn "%A")
*)


[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    0 // return an integer exit code

