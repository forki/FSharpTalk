let tprintfn id =
  printfn "Calc Task %d on Thread [%d]" id System.Threading.Thread.CurrentThread.ManagedThreadId

let task1 =
  async {
    tprintfn 1
    return 10 + 10 }
  
let task2 = 
  async  {
    tprintfn 2
    return 20 + 20}

[task1; task2]
  |> Async.Parallel
  |> Async.RunSynchronously
  |> printfn "%A"
