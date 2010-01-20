/// for the first 100 integers print "foo" if the number is a multiple of 3 
/// and print "bar" if is a multiple of 5

let foobarImperative n =
  for i in 1..n do
    printf "%d: " i
    if i % 3 = 0 then printf "foo"
    if i % 5 = 0 then printf "bar"
    printfn ""

foobarImperative 100

let foobar n =
  let foo x = if x % 3 = 0 then "foo" else ""
  let bar x = if x % 5 = 0 then "bar" else ""
  
  [1..n] 
    |> List.iter (fun i -> printfn "%d: %s" i (foo i + bar i))

foobar 100
