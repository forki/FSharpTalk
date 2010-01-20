open System.IO

// The 'use' binding indicates that the IDisposable.Dispose method should be called 
// on the object at the end of its lexical scope.
let FileTest() = 
  File.WriteAllLines(@"test.txt", 
    [| "This is a test file.";
       "It is easy to write."; 
       "And it is easy to read."; |]);

  use sr = File.OpenText @"test.txt"
  let line1 = sr.ReadLine() 
  let line2 = sr.ReadLine() 
  let line3 = sr.ReadLine() 
  printfn "line1 = %s" line1
  printfn "line2 = %s" line2
  printfn "line3 = %s" line3
  
FileTest()  