#r "FSharp.PowerPack.dll"

open System.IO
open System.Net
open Microsoft.FSharp.Control.WebExtensions

let AsyncHttp(url:string) =
  async {
      let req = WebRequest.Create url

      // Get the response, asynchronously
      let! rsp = req.AsyncGetResponse()

      use stream = rsp.GetResponseStream()
      use reader = new StreamReader(stream) 

      // read the bytes, synchronously
      return! reader.AsyncReadToEnd()}
  
    
SyncHttp "http://maps.google.com"
SyncHttp "http://maps.live.com"