/// A palindromic number reads the same both ways. 
/// The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 * 99.
///
/// Find the largest palindrome made from the product of two 3-digit numbers.

let isPalindrom (s:string) = 
  let a = s.ToCharArray()
  Array.rev a = a

let numbers n = 
  [for i in 1..n do
     for j in 1..n do
       let s = (i*j).ToString()
       yield i*j,i,j, isPalindrom s]
        
numbers 999 
  |> List.filter (fun (a,b,c,d) -> d)
  |> List.max 
  |> printfn "%A"