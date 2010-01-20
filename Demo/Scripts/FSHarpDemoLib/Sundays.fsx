open System

/// How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?
let days startDate endDate = 
  Seq.unfold  
      (fun (actDate:DateTime) -> 
         if actDate <= endDate then Some (actDate, actDate.AddDays(1.0)) else None)
      startDate

days (DateTime(1901,1,1)) (DateTime(2000,12,31))
  |> Seq.filter (fun day -> day.DayOfWeek = DayOfWeek.Sunday && day.Day = 1)
  |> Seq.length
  |> printfn "Days: %d"
  
#light

open System

// Aufgabe 10
// Finden Sie das größte Produkt von fünf aufeinanderfolgender Ziffern in der folgenden
// 1000-stelligen Zahl:

let number =
    "73167176531330624919225119674426574742355349194934" +
    "96983520312774506326239578318016984801869478851843" +
    "85861560789112949495459501737958331952853208805511" +
    "12540698747158523863050715693290963295227443043557" +
    "66896648950445244523161731856403098711121722383113" +
    "62229893423380308135336276614282806444486645238749" +
    "30358907296290491560440772390713810515859307960866" +
    "70172427121883998797908792274921901699720888093776" +
    "65727333001053367881220235421809751254540594752243" +
    "52584907711670556013604839586446706324415722155397" +
    "53697817977846174064955149290862569321978468622482" +
    "83972241375657056057490261407972968652414535100474" +
    "82166370484403199890008895243450658541227588666881" +
    "16427171479924442928230863465674813919123162824586" +
    "17866458359124566529476545682848912883142607690042" +
    "24219022671055626321111109370544217506941658960408" +
    "07198403850962455444362981230987879927244284909188" +
    "84580156166097919133875499200524063689912560717606" +
    "05886116467109405077541002256983155200055935729725" +
    "71636269561882670428252483600823257530420752963450";

// max = 9*9*9*9*9 = 59049


let rec GetMaxProduct (s:string) i f1 f2 f3 f4 f5 (m: int byref) =
  let p: int = f1 * f2 * f3 * f4 * f5
  if p > m then
    m <- p 
  if s.Length - i > 0 then      
    let fn = System.Int32.Parse(s.Substring(i,1))
    let j = i + 1 
    GetMaxProduct s j f2 f3 f4 f5 fn (&m)

    
let CalcMaxProduct (s:string) =
  let f1 = System.Int32.Parse(s.Substring(0,1))
  let f2 = System.Int32.Parse(s.Substring(1,1))
  let f3 = System.Int32.Parse(s.Substring(2,1))
  let f4 = System.Int32.Parse(s.Substring(3,1))
  let f5 = System.Int32.Parse(s.Substring(4,1))
  let mutable m = 0;
  GetMaxProduct s 5 f1 f2 f3 f4 f5 (&m) 
  m // return
  

printfn @"Max %d " (CalcMaxProduct number)

System.Console.ReadKey();   