#r @"..\lib\Rx\System.Reactive.dll"
#r @"..\lib\Rx\System.CoreEx.dll"
#r @"..\lib\Rx\System.Threading.dll"
#r @"System.Core.dll"
#r @"WindowsBase.dll"
#r "FSharp.PowerPack.dll"

#load "Observable.fs"
open RxExtensions
open Microsoft.FSharp.Control.WebExtensions
open System.IO
open System.Net

let AsyncHttp (url:string) =
  async { 
    // using asynchronous workflows
    let request = WebRequest.Create url
    // async call of GetResponse
    let! response = request.AsyncGetResponse()
    use stream = response.GetResponseStream()
    use reader = new StreamReader(stream)
    // async call of ReadToEnd
    let! result = reader.AsyncReadToEnd()
    return url,result} 

/// Async<string * string> list
let downloads =
  ["http://www.google.com";
   "http://www.twitter.com";
   "http://www.nytimes.com/";
   "http://www.navision-blog.de/";
   "http://www.nba.com/"] 
    |> List.map AsyncHttp

/// IObservable<string * string>
let observableDownloads =
  downloads
    |> Seq.map Observable.ofAsync
    |> Observable.merge

observableDownloads
  |> Observable.subscribe (fun (url,result) ->
       printfn "%A: %d" url result.Length)