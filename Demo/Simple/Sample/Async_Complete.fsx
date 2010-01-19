#r "FSharp.PowerPack.dll"

let tprintfn fmt =
  printf "[%d]" System.Threading.Thread.CurrentThread.ManagedThreadId
  printfn fmt

let task1 = async { 
               tprintfn "Calc task1"
               return 10+10 }
let task2 = async { 
               tprintfn "Calc task2"
               return 20+20 }

[task1; task2]
  |> Async.Parallel
  |> Async.RunSynchronously
  |> printfn "%A"  
  
  
//  WebRequest

open System.IO
open System.Windows.Forms
open System.Net
open Microsoft.FSharp.Control.WebExtensions


let SyncHttp(url:string) =
  let req = WebRequest.Create url

  // Get the response, synchronously
  let rsp = req.GetResponse()

  use stream = rsp.GetResponseStream()
  use reader = new StreamReader(stream)

  reader.ReadToEnd()
    
SyncHttp "http://maps.google.com"
SyncHttp "http://maps.live.com"  

let AsyncHttp(url:string) =
  async {
    let req = WebRequest.Create url

    // Get the response, asynchronously
    let! rsp = req.AsyncGetResponse()

    use stream = rsp.GetResponseStream()
    use reader = new System.IO.StreamReader(stream)

    // asynchronous read-to-end
   return! reader.AsyncReadToEnd() }

[ AsyncHttp "http://www.live.com";
  AsyncHttp "http://www.google.com";
  AsyncHttp "http://maps.live.com";
  AsyncHttp "http://maps.google.com"; ]  
    |> Async.Parallel
    |> Async.RunSynchronously
    |> printfn "%A"