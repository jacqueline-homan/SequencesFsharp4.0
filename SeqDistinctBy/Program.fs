open System
open System.IO

type Vegetable =
    { Name : string 
      Color : string } 
//Our collection of available veggies
let availableVeggies = 
    [| { Name = "Carrot "
         Color = "orange" }
       { Name = "Parsnips" 
         Color = "yellow" }
       { Name = "Beet" 
         Color = "purple" } 
       { Name = "Potato" 
         Color = "white" }
       { Name = "Cabbage" 
         Color = "green" } 
       { Name = "Tomato" 
         Color = "red" }
       { Name = "Turnip" 
         Color = "white" }
       { Name = "Squash" 
         Color = "yellow" }
       { Name = "Corn" 
         Color = "Yellow" }
       { Name = "Yam" 
         Color = "orange" }
       { Name = "Rhubarb" 
         Color = "red" }
       { Name = "Broccoli" 
         Color = "green" }
    |]
//Randomizes our list of veggies
let randomize s =
    let r = new Random()
    s
    |> Seq.sortBy(fun _ -> r.Next())

//Counts the number of each veggie by color
let veggieColorCount() =
    availableVeggies
    |> Seq.countBy(fun veggie -> veggie.Color)
veggieColorCount() |> Seq.iter(printfn "%A")

//Our randomly generated meal menu
let menu() =
    availableVeggies
    |> randomize 
    |> Seq.distinctBy( fun veggie -> veggie.Color)

menu() |> Seq.iter(printfn "%A") 


[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    0 // return an integer exit code

