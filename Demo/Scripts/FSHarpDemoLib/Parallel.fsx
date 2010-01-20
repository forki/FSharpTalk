#r "System.Threading"
#r "System.Core"
#r "System.Numerics"

open System
open System.Diagnostics
open Microsoft.FSharp.Math
open System.Numerics

let testRuntime f =
  let watch = new Stopwatch()
  watch.Start()
  (f(),watch.Elapsed)
  
let fac (x:BigInteger) = [1I..x] |> List.fold (*) 1I

let sequentialSum() =
  [1I..3000I]
    |> List.map fac
    |> List.fold (+) 0I
    
let sumSequential,timeSequential = testRuntime sequentialSum
printfn "Time Sequential: %.3fs" timeSequential.TotalSeconds 
printfn "Result: %A" sumSequential

//  -------------------------------------------------------
//  Using PLINQ

open System.Linq

/// Type wrapper over the IParallelEnumerable
type pseq<'a> = ParallelQuery<'a> 

/// This is the method to opt into Parallel LINQ.
let adapt (x : seq<'a>) : pseq<'a> = x.AsParallel()

/// Parallel implementation of System.Linq.Enumerable.Select().
let map (f:'a -> 'b) (pe:pseq<'a>) : pseq<'b> = 
  ParallelEnumerable.Select(pe, Func<_,_>(f))  

/// Parallel implementation of System.Linq.Enumerable.Aggregate().
let fold (f:'a -> 'b -> 'a) (seed:'a) (pe:pseq<'b>) : 'a = 
  ParallelEnumerable.Aggregate(pe, seed, Func<_,_,_>(f)) 

// -------------------------------------------------------
let parallelSum() =
  [1I..3000I]
    |> adapt
    |> map fac 
    |> fold (+) 0I
    
let sumPLINQ,timePLINQ = testRuntime parallelSum
printfn "Time PLINQ: %.3fs" timePLINQ.TotalSeconds 
printfn "Result: %A" sumSequential
