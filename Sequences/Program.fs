open System
open System.IO
open System.Net
//Creating sequences from:
// using a range and invoking the seq expression
let integers1 = seq { for i in 1..10 do yield i}
let integers2 = seq { for i in 1..10 -> i} 

// using the Seq module's functions, init or initInfinite
let integers3 = Seq.init 10 (fun i -> i + 1)
let integers4 = Seq.initInfinite (fun x -> x + 1)

(*let integers5 = 
    [ for n in 1..10 -> n]
    |> Seq.initInfinite(fun x -> (x * 1) / x = 0)

let isMultipleOf x y = fun x y -> x * y

[for i in 1..10 -> integers5] |> printfn "%A"  *)

// Using Seq.map to display extensions of file types in a directory
let extensions (dir:string) =
    Directory.EnumerateFiles(dir)
    |> Seq.map(fun name -> Path.GetExtension(name))
    |> Seq.distinct
 
printfn "%A" (extensions (@"/home/jacque/Documents")) 

//Pattern-matching for producing a Morse Code sequence
let morseCode(s:string)=
    let charMorse c =
        match Char.ToUpper(c) with
        | 'A' -> ".-"
        | 'B' -> "-..."
        | 'C' -> ""
        | _ -> ""
    s
    |> Seq.map charMorse
    |> Seq.reduce (+)



// Legally violating F#'s immutable sequence values rule:
let seqWithRef =
    seq { 
        let i = ref 0 
        while !i < 100 do
            yield !i 
            i := !i + 1
        }

(*Recursively traversing files using F# sequence operations and .NET recrusive
File traversal *)

let fileHierarchy startDir =
    Directory.EnumerateFiles(startDir, @"*.*", SearchOption.AllDirectories)
fileHierarchy @"/home/jacque/Projects/F-sharp/Sequences"
|> Array.ofSeq
|> Seq.iter(printfn "%A")

let rec safeFileSearch startDir =
    let tryEnumerateFiles dir =
        try 
            Directory.EnumerateFiles(startDir)
        with _ -> Seq.empty 
    let tryEnumerateDirs dir =
        try 
            Directory.EnumerateDirectories(startDir) 
        with _ -> Seq.empty 
    seq { 
        yield! tryEnumerateFiles startDir 
        for dir in tryEnumerateDirs startDir do 
            yield! (safeFileSearch dir) 
        } 

safeFileSearch @"/home/jacque/Projects/F-sharp/Sequences"
|> Seq.take(10) //You can also use Seq.truncate, either one will work
|> Array.ofSeq
//|> Seq.item (100)         //In lieu of Seq.nth
|> Seq.iter(printfn "%A")
 
printfn " "
//Using Seq.pick or tryPick
let randomRealURL() =
    let rnd = System.Random()
    let randomUrl len =
        let randAlpha() = rnd.Next(int('a'), int('z') + 1) |> char
        let randChars = Array.init len (fun _ -> randAlpha())
        let randString = new String(randChars)
        sprintf "http://www.%s.com" randString
    let urls = Seq.initInfinite(fun _ -> randomUrl 7)
    use wc = new WebClient()
//Our core algoritm starts here
    urls 
    |> Seq.pick(fun url -> 
        try 
            printfn "Trying %s" url 
            wc.DownloadString(url) |> ignore 
            url |> Some 
        with _ -> None)
   
//Call our function to find random urls with a length of 7 characters
randomRealURL() |> printfn "%s"  //If you don't want output, use |> ignore

// This can go on for waaaay too long!
(* 
let findMyNumbers() =
    let rnd = new Random()

    let isDivisibleBy7 n =
        let rec check i =
            i >= n/7 || (n%i <> 0 && check (i + 2))
        check 7
    let numbers = Seq.initInfinite(fun _ -> rnd.Next())
    let steps =
        numbers
        |> Seq.findIndex(fun n -> 
            printfn "Trying %i" n
            isDivisibleBy7 n)
    steps + 1
findMyNumbers() |> printfn "%i"
*)  
printfn ""

let findMyPrimes() =
    let rnd = new Random()
  
    let isPrime n =
        let rec check i =
            i > n/2 || (n % i <> 0 && check (i + 1))
        check 2
    let numbers = Seq.initInfinite(fun _ -> rnd.Next())
    let steps =
        numbers
        |> Seq.findIndex(fun n -> 
            printfn "Trying %i" n
            isPrime n)
    steps + 1
findMyPrimes() |> printfn "%i" // Returns the total number of primes found

let numbers = [1;2;3;4;5;6;7;8;9;10;11;12;13]
let containsPrimes numbers =
    let isPrime n = 
        let rec check i =
            i > n / 2 || (n % i <> 0 && check (i + 1))
        check 2
    numbers 
    |> Seq.exists isPrime 
containsPrimes numbers |> printfn "%b"

printfn " "

let range0fNumbers = [0..100] 
let GaussianSum rangeOfNumbers =
    range0fNumbers
    |> List.filter(fun x -> x % 2 = 0)
    |> List.map(fun x -> x * 2)
    |> List.sum
GaussianSum range0fNumbers |> printfn "%i"


[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    0 // return an integer exit code

