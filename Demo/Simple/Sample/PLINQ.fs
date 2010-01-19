namespace PLINQ

module PSeq =

  open System
  open System.Linq
  open System.Collections.Generic

  type pseq<'a> = ParallelQuery<'a>

  /// Append two parallel collections together
  let append ie1 ie2 = ParallelEnumerable.Concat(ie1, ie2)

  /// This is the method to opt into Parallel LINQ.
  let adapt (x:seq<_>) = x.AsParallel()
    
  /// AsOrdered is a method that tells PLINQ to treat a data source as if it was ordered
  let ordered = ParallelEnumerable.AsOrdered
  
  /// AsUnordered tells PLINQ that it should treat a particular intermediate result as if no
  /// order was implied
  let unordered = ParallelEnumerable.AsUnordered
  
  /// This method is to opt out of Parallel LINQ.
  let as_seq = ParallelEnumerable.AsSequential
    
  /// Parallel implementation of System.Linq.ParallelEnumerable.Any
  let exists f pe = ParallelEnumerable.Any(pe, Func<_,_>(f))

  /// Parallel implementation of System.Linq.Enumerable.Where().
  let filter f pe = ParallelEnumerable.Where(pe, Func<_,_>(f))
    
  /// Parallel implementation of System.Linq.Enumerable.First
  let find f pe = ParallelEnumerable.First(pe, Func<_,_>(f)) 

  /// Parallel implementation of System.Linq.Enumerable.Aggregate().
  let fold f seed pe = 
    ParallelEnumerable.Aggregate(pe, seed, Func<_,_,_>(f))
    
  /// Parallel implementation of System.Linq.Enumerable.All
  let forAll f pe = ParallelEnumerable.All(pe, Func<_,_>(f))
        
  /// Empty ParallelEnumerable
  let empty<'a> = ParallelEnumerable.Empty<'a>()
    
  /// Parallel implementation of System.Linq.Enumerable.First()
  let head = ParallelEnumerable.First
      
  /// Parallel implementation of System.Linq.ParallelEnumerable.Count
  let length = ParallelEnumerable.Count  
    
  /// Parallel implementation of System.Linq.Enumerable.Select().
  let map f pe = ParallelEnumerable.Select(pe, Func<_,_>(f))           
    
  /// Parallel implementation of System.Linq.Enumerable.Select().
  let mapi f pe =
    let f' x i = f i x
    ParallelEnumerable.Select(pe, Func<_,_,_>(f'))
    
  /// Parallel implementation of System.Linq.Enumerable.Zip
  let map2 f pe1 pe2 =
    ParallelEnumerable.Zip(pe1, pe2, Func<_,_,_>(f))
    
  /// Parallel implementation of System.Linq.Enumerable.Reverse
  let rev = ParallelEnumerable.Reverse
      
  /// Parallel implementation of Seq.concat
  let concat pe = 
    ParallelEnumerable.SelectMany(
        pe, Func<_,_>(fun x -> x :> seq<'a>))
    
  /// Parallel implementation of System.Linq.Enumerable.ElementAt
  let nth n pe =
    ParallelEnumerable.ElementAt(pe, n)
    
  /// Parallel implementation of System.Linq.Enumerable.OrderBy
  let order_by f pe =
    ParallelEnumerable.OrderBy(pe, Func<_,_>(f))
    
  /// Parallel implementation of System.Linq.Enumerable.Range
  let range start count =
    ParallelEnumerable.Range(start, count)
    
  /// Parallel implementation of System.Linq.Enumerable.Skip
  let skip n pe = ParallelEnumerable.Skip(pe, n)
    
  /// Parallel implementation of System.Linq.Enumerable.SkipWhile
  let skip_while f pe =
    ParallelEnumerable.SkipWhile(pe, Func<_,_>(f))
    
  /// Parallel implementation of System.Linq.Enumerable.Sum().
  let sum_by_int (x:ParallelQuery<int>) = ParallelEnumerable.Sum(x)          
    
  /// Parallel implementation of System.Linq.Enumerable.Take
  let take n pe = ParallelEnumerable.Take(pe, n)
      
  /// Parallel implementation of System.Linq.Enumerable.TakeWhile
  let take_while f pe =
    ParallelEnumerable.TakeWhile(pe, Func<_,_>(f))
  
  /// Parallel implementation of System.Linq.Enumerable.ToArray().
  let toArray = ParallelEnumerable.ToArray
    
  /// Parallel implementation of to list  
  let toList<'a> = toArray >> Array.toList    
      
  /// Parallel implementation of System.Linq.Enumerable.Zip
  let zip pe1 pe2=
    let tuple a b = a, b
    ParallelEnumerable.Zip(pe1, pe2, Func<_,_,_>(tuple))             
      
[<AutoOpen>]
module Operators =
  
  /// Used for sequence expressions
  /// Example : let f = pseq [for x in [1..10] -> x * x] 
  let pseq (ie:seq<'a>) : PSeq.pseq<'a> = PSeq.adapt ie