// Discriminated unions give a way of building types from the disjoint union of two existing types. 
// This sample shows how to build one such type and how to decompose its values.

type wheel = Wheel of float  // radius of wheel, inches
type cycle = 
  | Unicycle of wheel
  | Bicycle of wheel * wheel 

let veryBigWheel = Wheel(26.0)
let bigWheel     = Wheel(13.0)
let smallWheel   = Wheel(6.0)

let myBike        = Unicycle(veryBigWheel)
let racer         = Bicycle(bigWheel    ,bigWheel)
let kidsBike      = Bicycle(smallWheel  ,smallWheel)

let show bike = 
  match bike with 
    | Unicycle (Wheel r) -> printfn "Unicycle, one wheel, radius = %f" r
    | Bicycle (Wheel r1,Wheel r2) -> printfn "Bicycle, two wheels, front = %f, back = %f" r1 r2 
  
show myBike
show racer
show kidsBike
