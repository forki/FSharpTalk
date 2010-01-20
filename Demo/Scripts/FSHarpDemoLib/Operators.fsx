#r "FSharp.PowerPack"

// ---------------------------------------------------
/// Returns the div and modulo of an integer division
let (/%) a b = a / b, a % b

let res = 10 /% 3
printfn "10 /%% 3 = %A" res

// ---------------------------------------------------
// Ruby collection operators

/// Concatenation of two list
let (.+) left right = List.append left right

[1;2;3;4] .+ [3;4;5;6] |> printfn "%A"
[1;2;3;4]  @ [3;4;5;6] |> printfn "%A"

/// Calculates the difference of two lists
let (.-) left (right:'a list) =
  let mem = HashSet<_>(right)
  left |> List.filter (fun n -> not (mem.Contains n))

[1;2;3;4] .- [3;4;5;6] |> printfn "%A"

/// calculates the intersect of two lists
let (.&) left (right:'a list) =
  let mem = HashSet<_>(right)
  left |> Seq.filter (fun n -> mem.Contains n)

[1;2;3;4] .& [3;4;5;6] |> printfn "%A"


/// calculates the union of two lists
/// Union is concatenation without duplicates
let (.|) left right =
  left .+ right |> Seq.distinct |> List.of_seq

[1;2;3;4] .| [3;4;5;6] |> printfn "%A"

/// Produces a n-times repetition of the list
let (.*) n list =
  [1 .. n] |> List.fold (fun acc i -> acc .+ list) []

3 .* [1;2;3] |> printfn "%A"

// ---------------------------------------------------

open System.Text.RegularExpressions

/// Regular Expression Binding Operator
let (=~) s pattern = System.Text.RegularExpressions.Regex.IsMatch(s,pattern)

"Mississippi" =~ "M(i(ss|pp)*)+" |> printfn "%A"

/// Functional Binding Operator (=~~)
let (=~~) s pattern =
  let m = Regex.Match(s,pattern)
  if m.Success then Some m else None


let testMatch s pattern =
  match s =~~ pattern with
    | Some m -> printf "%s\n" (m.ToString())
    | None -> printf "No Match"   
    
testMatch "Mississippi" "^M(i(ss|pp)*)+"    
testMatch "bla Mississippi" "^M(i(ss|pp)*)+"    
