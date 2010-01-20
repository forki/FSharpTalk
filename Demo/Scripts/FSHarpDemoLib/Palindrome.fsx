/// A palindromic number reads the same both ways. 
/// The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 * 99.
///
/// Find the largest palindrome made from the product of two 3-digit numbers.

let isPalindrome (s:string) = 
  let a = s.ToCharArray()
  Array.rev a = a

let numbers n = 
  seq{for a in 1..n do
       let i = n - a
       for b in 1..n do        
         let j = n - b
         let s = (i*j).ToString()
         yield i*j,i,j, isPalindrome s}
        
numbers 999 
  |> Seq.filter (fun (a,b,c,d) -> d)
  |> Seq.take(1)
  |> printfn "%A"