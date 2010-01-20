type Tree<'a> = 
| Node of Tree<'a> * Tree<'a> 
| Leaf of 'a

let rec sum1 = function
    | Node (l,r) -> sum1 l + sum1 r  // not tail-recursive!
    | Leaf x     -> x
    
let rec inorder nodeF leafF = function
    | Node (l,r) -> 
        let left =  inorder nodeF leafF l  // not tail-recursive!
        let right = inorder nodeF leafF r
        nodeF left right
    | Leaf x -> leafF x
    
let sum tree = inorder (+) id tree    