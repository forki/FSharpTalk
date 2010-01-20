// interface sample

type IPoint = 
  abstract X : float
  abstract Y : float


/// Implementing an interface with an object expression.
let Point(x,y) =
  { new IPoint with 
     member obj.X = x 
     member obj.Y = y }

/// Implementing an interface with an object expression that has mutable state
let MutablePoint(x,y) =
  let currX = ref x 
  let currY = ref y
  { new IPoint with 
     member obj.X = currX.Value 
     member obj.Y = currY.Value }
     
     
/// This interface is really just a function since it only has one 
/// member, but we give it a name here as an example. It represents 
/// a function from some variable to (X,Y)
type ILine = abstract Get : float -> IPoint

/// Implementing an interface with an object expression.
///
/// Here a line is specified by gradient/intercept
let Line(a:float,c:float) = 
  let y(x) = a * x + c
  { new ILine with 
      member l.Get(x) = Point(x, y(x)) }
 
/// Implementing an interface with a class.
///
/// Here a line is specified by gradient/intercept
type GradientInterceptLine(a:float,c:float) = 
  // Some local bindings
  let y(x) = a * x + c

  // Publish additional properties of the object
  member x.Gradient = a
  member x.Intercept = c

  // Also implement the interface
  interface ILine with 
    member l.Get(x) = Point (x,y(x))     