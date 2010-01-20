#r "System.Numerics"
#load "TimeHelper.fs"
open System.Collections.Generic
open Demo
open Microsoft.FSharp.Math
open System.Numerics
#nowarn "40"

/// How many values of C(n,k), for 1 ≤ n ≤ 100, exceed one-million?
/// Remark: C(n,k) are the binomial coefficients.

/// binomial over factorials
let fac x = List.fold (*) 1I [1I..x]
let binomial_coefficient n k = fac n / (fac k * fac (n-k))

/// binomial over pascal's triangle
let rec binomial(n,k) =
  if k = 0I || n = k then 1I else
  binomial(n-1I, k) + binomial(n-1I, k-1I)

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
        
let rec binomialMemoized =
  let f (n,k) =  
    if k = 0I || n = k then 1I else
    binomialMemoized(n - 1I,k) + binomialMemoized(n - 1I,k - 1I)
  
  f |> memoize

start "How many values of C(n,k), for 1 ≤ n ≤ 100, exceed one-million?"
  
seq { for n in 2I .. 100I do
        for k in 2I .. n -> n,k }
   |> Seq.filter (fun (a,b) -> binomialMemoized(a,b) > 1000000I )
   |> Seq.length
   |> solved
   
   

