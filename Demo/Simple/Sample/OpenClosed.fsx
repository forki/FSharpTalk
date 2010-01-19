type EMail =
  { Sender: string;
    Recipient: string;
    Subject: string;
    Body: string}

type SpamResult =
  | Spam
  | OK
  | Unknown

let checkMail rules (mail:EMail) =
  let rec checkRule rules =
    match rules with
    | rule::rest ->
          match rule mail with
          | Unknown    -> checkRule rest
          | other -> other
    | [] -> Unknown

  checkRule rules
  

// concrete ruleChecker
  
let myFirstRule mail =
  // I don’t care about this
  Unknown

let mySecondRule mail =
  // I don’t care about this
  Spam

let ruleChecker =
  checkMail
    [ myFirstRule;
      mySecondRule]
  
  
// using option types
type SpamResult2 =
  | Spam
  | OK
  
let checkRules rules element =
  let rec checkRule rules =
    match rules with
    | rule::rest ->
          match rule element with
          | None -> checkRule rest
          | _ as other -> other
    | [] -> None

  checkRule rules  
  
// concrete ruleChecker  
let myRule3 (mail:EMail) =
  // I don’t care about this
  None

let myRule4 mail =
  // I don’t care about this
  Some Spam

let ruleChecker2 =
  checkRules
    [ myRule3;
      myRule4]           
             
// using higher-order functions     

let checkRules2 rules element =
  List.tryPick ((|>) element) rules
  
// http://www.navision-blog.de       