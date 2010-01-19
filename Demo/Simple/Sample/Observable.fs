module Observable

/// Creates an observer with the given functions
let createObserver next error completed =
    {new System.IObserver<_> with 
        member this.OnCompleted() = completed()
        member this.OnError(e) = error e
        member this.OnNext(args) = next args}

/// Subscribes an observer with the given functions 
///   param1: OnNext        (T -> unit)
///   param2: OnError       (Exception -> unit)
///   param3: OnCompleted   (unit -> unit)
///   param4: observable
let subscribeComplete next error completed (observable:System.IObservable<_>) =
  createObserver next error completed
    |> observable.Subscribe
    


      
      