open System
open System.Net
open System.Text.RegularExpressions

let uniqueWords() =
    let urls = 
        [| "http://www.google.com"
           "http://www.lynda.com"
           "http://www.amazon.com"
        |]
    let werdz(s:string) =
        let re = new Regex(@"\b\w+\b")
        let words = re.Matches(s)
        words
        |> Seq.cast 
        |> Seq.map(fun w -> w.ToString())

    use wc = new WebClient()
    urls
    |> Seq.choose(fun url -> 
        try 
            wc.DownloadString(url) |> Some 
        with _ -> None)
    |> Seq.collect werdz
    |> Seq.distinct
    |> Seq.sort
    |> Seq.iter(fun w -> printfn "%s" w)

uniqueWords()

[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    0 // return an integer exit code

