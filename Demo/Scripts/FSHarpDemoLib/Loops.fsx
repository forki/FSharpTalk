// simple 'for' loops

for i in 1..10 do 
  printfn "In a for-loop, i = %d" i

// iterate through a list        
for i in ["quick"; "brown"; "fox"] do 
  printfn "i = %s" i

// nested loops  
for i in 0 .. 9 do 
  for j = 0 to i-1 do 
    printfn " "
  for j = i to 9 do 
    printfn "%d" j
  printfn ""  
  
  
// while loop
open System
  
let start = DateTime.Now 
let duration = TimeSpan.FromSeconds(2.0)
printfn "Waiting..."

// Here's the loop
while DateTime.Now - start < duration do
  printfn "."

// OK, we're done...
let span = DateTime.Now - start 
printfn "\nAttempted to busy-wait 2s, actually waited %.2f ms" span.TotalSeconds
 
// recursion
let rec fib n = 
  if n < 2 then 1 else fib(n-1) + fib(n-2) 
    
for i = 0 to 10 do
  printfn "fib %d = %d" i (fib i)