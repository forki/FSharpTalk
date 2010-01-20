// link on http://finance.yahoo.com/q/hp?s=MSFT

open System
open System.Net
open System.IO

let ticker = "MSFT"
let url = "http://ichart.finance.yahoo.com/table.csv?s=" + ticker + "&d=4&e=19&f=2009&g=d&a=2&b=13&c=1986&ignore=.csv"

let req = WebRequest.Create(url)
let resp = req.GetResponse()
let stream = resp.GetResponseStream()
let reader = new StreamReader(stream)
let csv = reader.ReadToEnd()

let prices =
  csv.Split([|'\n'|])
    |> Seq.skip 1
    |> Seq.map (fun line -> line.Split([|','|]))
    |> Seq.filter (fun values -> values |> Seq.length = 7) 
    |> Seq.map (fun values -> DateTime.Parse(values.[0]), float values.[6])

printfn "%A" csv           
printfn "%A" prices
    
    
/// making it a function

let asyncPrices (url:string) =
  let req = WebRequest.Create(url)
  let resp = req.GetResponse()
  let stream = resp.GetResponseStream()
  let reader = new StreamReader(stream)
  let csv = reader.ReadToEnd()

  let prices =
    csv.Split([|'\n'|])
      |> Seq.skip 1
      |> Seq.map (fun line -> line.Split([|','|]))
      |> Seq.filter (fun values -> values |> Seq.length = 7) 
      |> Seq.map (fun values -> DateTime.Parse(values.[0]), float values.[6])
  prices

  