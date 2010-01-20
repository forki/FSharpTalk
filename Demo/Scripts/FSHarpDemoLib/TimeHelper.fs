namespace Demo

open System 
open System.Diagnostics  

[<AutoOpen>]
module Timer = 
  let stopWatch = new Stopwatch() 
  let ResetStopWatch() = 
    stopWatch.Reset()
    stopWatch.Start() 
  let ShowTime f = 
    printfn "%s took %d ms" f stopWatch.ElapsedMilliseconds 
    
  let start s =  
    ResetStopWatch()
    printfn "\r\n---------------------------------------------------"
    printfn "%s" s

  let solved result = 
    ShowTime "Calculation"
    printfn "  ==> %A" result     