namespace Sample

module TestData =
    let list1 = [1; 2; 42; 100]

    let randomList typeF n =
      let r = new System.Random()
      [for i in 1..n -> typeF(r.Next 100)]
      
    let bigint (x:int) = Math.bigint x
    let list2 = randomList int 10000

    let list3 = randomList int 10000000