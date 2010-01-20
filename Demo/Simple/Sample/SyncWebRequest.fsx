#r "FSharp.PowerPack.dll"

open System.IO
open System.Net
open Microsoft.FSharp.Control.WebExtensions

let SyncHttp(url:string) =
  let req = WebRequest.Create url

  // Get the response, synchronously
  let rsp = req.GetResponse()

  use stream = rsp.GetResponseStream()
  use reader = new StreamReader(stream) 

  // read the bytes, synchronously
  reader.ReadToEnd()
  
    
SyncHttp "http://maps.google.com"
SyncHttp "http://maps.live.com"