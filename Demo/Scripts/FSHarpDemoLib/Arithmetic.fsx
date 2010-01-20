/// A very simple constant integer
let int1 = 1

/// A second very simple constant integer
let int2 = 2

/// Add two integers
let int3 = int1 + int2

/// A function on integers
let f x = 2*x*x - 5*x + 3

/// The result of a simple computation 
let result = f (int3 + 4)

/// Another function on integers
let increment x = x + 1

// Some basic integer arithmetic
let x = 10 + 12 - 3 
let y = x * 2 + 1 

// multiple let bindings (tupling and pattern matching)
let fBigInt x y = x/3, x%3
let r1,r2 = x/3, x%3

// print the values:
printfn "x = %d, y = %d, r1 = %d, r2 = %d" x y r1 r2 

// the same with floats
let x2 = 10.0 + 12.0 - 3.0 
let y2 = x2 * 2.0 + 1.0 
let r3 = x2 / 3.0
printfn "x = %g, y = %g, r1 = %g" x2 y2 r3

// arithmetic functions

let twice x = x + x 
let sq x = x * x

printfn "twice 2 = %d" (twice 2)
printfn "twice 4 = %d" (twice 4)
printfn "twice (twice 3) = %d" (twice (twice 3))

// inner functions
let even n = (n%2 = 0) 
let tick x = printfn "tick %d" x 
let tock x = printfn "tock %d" x
let choose f g h x = if f x then g x else h x 
let ticktock = choose even tick tock  
// ticktock is a function built out of other functions using 'choose'

for i = 0 to 10 do 
  ticktock i

// lamba expressions  
for i = 0 to 10 do
  // This is like the previous sample, 
  // but uses an anonymous lambda expression for 
  // the function that decides whether to tick or tock.
  choose (fun n -> n%2 = 0) tick tock i   
  
  
// Bitwise operators
let bitwise = 
  let x1 = 0xAB7F3456 &&& 0xFFFF0000 
  let x2 = 0xAB7F3456 ||| 0xFFFF0000 
  let x3 = 0x12343456 ^^^ 0xFFFF0000 
  let x4 = 0x1234ABCD <<< 1 
  let x5 = 0x1234ABCD >>> 16 

  // Also over other integral types, e.g. Int64:
  let x6 = 0x0A0A0A0A012343456L &&& 0x00000000FFFF0000L 

  // Also over other integral types, e.g. unsigned Int64:
  let x6u = 0x0A0A0A0A012343456UL &&& 0x0000FFFF00000000UL 

  // And bytes:
  let x7 = 0x13uy &&& 0x11uy 

  printfn "x1 = 0x%08x" x1;
  printfn "x2 = 0x%08x" x2;
  printfn "x3 = 0x%08x" x3;
  printfn "x4 = 0x%08x" x4;
  printfn "x5 = 0x%08x" x5;
  printfn "x6 = 0x%016x" x6;
  printfn "x6u = 0x%016x" x6u;
  printfn "x7 = 0x%02x" (int x7)
  
/// returns the greates common devisor
let rec gcd a b = if b = 0I then a else gcd b (a%b)
/// returns the least common multiple
let lcm a b = a * b / (gcd a b)
