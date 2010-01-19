module RxExtensions.Observable

open System.Linq
open System
open System.Threading
open System.Windows.Threading

type 'a observable = IObservable<'a>
type 'a observer = IObserver<'a>

/// converts a lambda in a System.Action
let asAction f = new System.Action(f)

/// System.Action whichs does nothing
let doNothing = asAction (fun () -> ())

/// Creates an observer
let createObserver next error completed =
  {new System.IObserver<_> with
      member this.OnCompleted() = completed()
      member this.OnError(e) = error e
      member this.OnNext(args) = next args}
      
/// Creates a new observable 
let create f : 'a observable =
  Observable.Create<_>(fun x ->
    f x
    doNothing)   

/// Creates a observable from a async
let ofAsync async =
  create
    (fun obs ->
       Async.StartWithContinuations
         (async,obs.OnNext,obs.OnError,obs.OnError))

/// Gets a dispatcher Schdeuler for the current dispatcher
let getDispatcherScheduler _ = 
  new DispatcherScheduler(Dispatcher.CurrentDispatcher)

/// Generates an observable from an IEvent
let fromEvent (event:IEvent<_,_>) = create (fun x -> event.Add x.OnNext)

/// Generates an empty observable 
let empty<'a> = Observable.Empty<'a>() 

/// Takes the head of the elements
let head = Observable.First 

/// Merges the two observables
let mergeWith obs1 obs2 = Observable.Merge(obs2, obs1)

/// Merges all observables
let mergeAll (observables:IObservable<IObservable<'a>>) = 
  Observable.Merge observables 

/// Merges all observables
let merge (observables:(IObservable<'a>) seq) = 
  Observable.Merge observables 

/// Creates a range as an observable
let range start count = Observable.Range(start, count)
      
/// Converts a seq in an observable
let toObservable (seq: 'a seq) = Observable.ToObservable seq

/// Converts a observable in a seq
let toEnumerable = Observable.ToEnumerable

/// Subscribes to the Observable with all 3 callbacks
let subscribeComplete next error completed (observable: 'a observable) = 
   observable.Subscribe(
     (fun x -> next x), 
     (fun e -> error e), 
     (fun () -> completed()))

/// Subscribes to the Observable with a
/// next and an error-function
let subscribeWithError next error observable = 
  subscribeComplete next error (fun () -> ()) observable
  
/// Subscribes to the Observable with just a next-function
let subscribe next observable = 
  subscribeWithError next ignore observable
    
/// throttles the observable for the given interval
let throttle interval observable = 
  Observable.Throttle(observable,interval)    

/// throttles the observable scheduled on the current dispatcher
let throttleOnCurrentDispatcher interval observable = 
  Observable.Throttle(
     observable,getDispatcherScheduler(),interval) 
        
/// samples the observable at the given interval
let sample interval observable = 
  Observable.Sample(observable,interval)    

/// samples the observable at the given interval 
/// scheduled on the current dispatcher
let sampleOnCurrentDispatcher interval observable = 
  Observable.Sample(
    observable,getDispatcherScheduler(),interval) 
  
/// returns the observable sequence that reacts first.
let takeFirstOf2Reactions obs1 obs2 = 
  Observable.Amb(obs1,obs2)

/// returns the observable sequence that reacts first.
let amb (obs: IObservable<'a> seq) = 
  Observable.Amb obs   

/// returns the observable sequence that reacts first.
let takeFirstReaction (obs: IObservable<'a> seq) = 
  Observable.Amb obs   

/// Matches when both observable sequences 
/// have an available value. 
let both obs1 obs2 = Observable.And(obs1,obs2)

let tuple a b = a,b
  
/// Merges two observable sequences
/// into one observable sequence.
let zip obs1 obs2 =    
   Observable.Zip(obs1, obs2, Func<_,_,_>(tuple))
   
/// Merges two observable sequences into one observable sequence 
/// whenever one of the observable sequences has a new value.
///    ==> More results than zip
let combineLatest obs1 obs2 =    
   Observable.CombineLatest(
     obs1, obs2, Func<_,_,_>(tuple))     

/// Concats the many observables to one observable
let concat (observable:IObservable<IObservable<'a>>) = 
  Observable.SelectMany(observable,Func<_,_>(id)) 

/// maps the given observable with the given function
let map f observable = 
  Observable.Select(observable,Func<_,_>(f))   

/// maps the given observable with the given function
let mapi f observable = 
  Observable.Select(observable,Func<_,_,_>(fun x i ->f i x))   

/// Filters all elements where the given predicate is satified
let filter f observable = 
  Observable.Where(observable, Func<_,_>(f)) 

/// Splits the observable into two observables
/// Containing the elements for which the predicate returns
/// true and false respectively
let partition predicate observable =
  filter predicate observable,
  filter (predicate >> not) observable

/// Skips n elements
let skip n observable = Observable.Skip(observable, n)

/// Skips elements while the predicate is satisfied
let skipWhile f observable = 
  Observable.SkipWhile(observable, Func<_,_>(f)) 
  
/// Runs all observable sequences in parallel 
/// and combines their first values. 
let forkJoin (observables: ('a observable) seq) =
  Observable.ForkJoin observables

/// Counts the elements
let length = Observable.Count

/// Takes n elements
let take n observable =
  Observable.Take(observable, n)   
  
/// Determines whether the given observable is empty  
let isEmpty observable = Observable.IsEmpty observable
  
/// Determines whether the given observable is not empty  
let isNotEmpty observable = not (Observable.IsEmpty observable)
  
/// Determines whether an observable sequence 
/// contains a specified value
/// which satisfies the given predicate
let exists predicate observable =
  observable
    |> skipWhile (predicate >> not)
    |> isNotEmpty

/// Continues an observable sequence that is terminated 
/// by an exception with the next observable sequence. 
let catch (newObservable:IObservable<'a>) failingObservable = 
  Observable.Catch(failingObservable,newObservable)  

/// Takes elements while the predicate is satisfied
let takeWhile f observable = 
  Observable.TakeWhile(observable, Func<_,_>(f)) 

/// Iterates through the observable 
/// and performs the given side-effect
let perform f observable =
  Observable.Do(observable,fun x -> f x)
   
/// Invokes finallyAction after source observable 
/// sequence terminates normally or by an exception. 
let performFinally f observable =
  Observable.Finally(observable,fun _ -> f())
  
/// Folds the observable
let fold f seed observable = 
  Observable.Aggregate(observable, seed, Func<_,_,_>(f))   

/// Retruns an observable from a async pattern  
let fromAsync beginF endF =
   Observable.FromAsyncPattern<_>(
     Func<_,_,_>(fun x y -> beginF(x,y)),
       (fun x -> endF x)).Invoke()
     
/// Runs all observable sequences in parallel 
/// and combines their first values. 
let subscribeAll next observables =
  observables |> Seq.map (subscribe next) |> Seq.toList      
     
type IObservable<'a> with
  /// Subscribes to the Observable with just a next-function
  member this.Subscribe(next) = 
    subscribe next this

  /// Subscribes to the Observable with a next 
  /// and an error-function
  member this.Subscribe(next,error) = 
    subscribeWithError next error this

  /// Subscribes to the Observable with all 3 callbacks
  member this.Subscribe(next,error,completed) = 
    subscribeComplete next error completed this
    
open System.Net

type WebRequest with
  member this.GetRequestStreamAsync() =
    fromAsync 
     this.BeginGetRequestStream 
     this.EndGetRequestStream
      
  member this.GetResponseAsync() =
    fromAsync 
      this.BeginGetResponse 
      this.EndGetResponse 
     
  member this.GetResponseStreamAsync() =
    fromAsync 
      this.BeginGetRequestStream 
      this.EndGetRequestStream
 
type Async<'a> with
  member this.ToObservable() = ofAsync this      