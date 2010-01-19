#r "System.Numerics"
#load "TestData.fs"  
open Sample.TestData

let sum0 list =
  let mutable sq = 0
  for x in list do
    sq <- sq + x
  sq

let rec sum1 list =
  match list with
  | [] -> 0
  | x :: rest -> x + sum1 rest


printfn "%A" (sum1 list2)

let rec sum2 acc list =
  match list with
  | [] -> acc
  | x :: rest -> sum2 (x+acc) rest

printfn "%A" (sum2 0 list3)
//
//let rec fold f acc list =
//  match list with
//  | [] -> acc
//  | x :: rest -> fold f (f acc x) rest

let sum3 list = List.fold (fun a b -> a + b) 0 list

let sq list = List.map (fun x -> x * x) list

let sumOfSq list =
  list
   |> Seq.map (fun x -> x * x)
   |> Seq.fold (+) 0

