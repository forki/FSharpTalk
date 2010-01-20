#load "TimeHelper.fs"
#r "System.Numerics"
open System.Collections.Generic
open Demo
open Microsoft.FSharp.Math

#nowarn "40"

//// Memoization sample 
let memoize f =
  let cache = Dictionary<_, _>()
  fun x ->
    match cache.TryGetValue(x) with
    | true, result -> result
    | false, _ ->
        let res = f x
        cache.[x] <- res
        res
        
let rec fib n =
  match n with
  | 0 -> 1
  | 1 -> 1
  | _ -> fib (n-1) + fib(n-2)
  
let rec fib2 =
  let f n = 
    match n with
    | 0 -> 1
    | 1 -> 1
    | _ -> fib2 (n-1) + fib2 (n-2) 
  f |> memoize
         
start "Calculate fib(40)."
  
fib2 40 |> solved
           