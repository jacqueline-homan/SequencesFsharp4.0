open System
open System.IO

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

let fileLength length = 
    if length < 1024L then "Small"
    elif length < 1024L * 1024L then "Medium"
    else "Ginormous"

let fileSize dir =
    dir 
    |> Directory.EnumerateFiles
    |> Seq.map(fun name -> 
        let info = new FileInfo(name)
        (name, info.Length))
    |> Seq.groupBy(fun(name, length) -> fileLength length)


let extensions (dir : string) =
    Directory.EnumerateFiles(dir)
    |> Seq.map (fun name -> Path.GetExtension(name))
    |> Seq.map (fun info -> info.Length)
    |> Seq.distinct


safeFileSearch @"/home/jacque/Documents"
|> Seq.truncate(10)
|> Array.ofSeq
|> Seq.iter(printfn "%A")

//Calling our function to print the distinct file extension names
extensions @"/home/jacque/Documents"
|> Seq.iter(printfn "%A")
(*let myGroupedFiles files =
    files
    |> fileLength
    |> fileSize
    |> string
    |> Array.ofSeq
    |> Seq.iter(printfn "%A") *)


fileSize @"/home/jacque/Documents"
|> Array.ofSeq
|> Seq.iter(printfn "%A")




[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    0 // return an integer exit code

