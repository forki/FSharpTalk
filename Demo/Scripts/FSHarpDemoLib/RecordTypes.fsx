// Record types

type Point = { x: float; y: float}
type Triangle = { p1: Point; p2: Point; p3: Point }

// Vector type with members
type Vector = 
  { dx: float; dy: float}
  static member Zero = { dx = 0.0; dy = 0.0 }
  static member OneX = { dx = 1.0; dy = 0.0 }
  static member OneY = { dx = 0.0; dy = 1.0 }
  static member (+) (v1,v2) = { dx = v1.dx + v2.dx; dy = v1.dy + v2.dy }
        
let origin = {new Point with x = 0.0 and y = 0.0 } // explicit
let onex = { x = 1.0; y = 0.0 }  // inferred
let oney = { x = 0.0; y = 1.0 }
let diff p1 p2 = { dx = p2.x - p1.x; dy = p2.y - p1.y }

let sides tri = 
  diff tri.p2 tri.p1, 
  diff tri.p3 tri.p2, 
  diff tri.p1 tri.p3

let triangle1 = { p1=origin; p2=onex; p3=oney } 
printfn "triangle1 = \n%A" triangle1
printfn "sides(triangle1) = \n%A" (sides triangle1)

let a = Vector.OneX
let b = {dx = 4.; dy =3.}
printfn "Verctor addition: %A + %A = %A" a b (a+b)
