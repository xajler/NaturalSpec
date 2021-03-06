﻿module Euler.Problem2 
  
open NaturalSpec

// Problem 2
// Each new term in the Fibonacci sequence is generated by adding the previous two terms. 
// By starting with 1 and 2, the first 10 terms will be:
//
// 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
//
// Find the sum of all the even-valued terms in the sequence which do not exceed four million.

let SumOfEvenValuedTermsInTheFibonacciSequence max =
    printMethod ""
    fibs
      |> Seq.filter (fun x -> x % 2I = 0I)
      |> Seq.takeWhile (fun x -> x <= max)
      |> Seq.sum
    
[<Scenario>]      
let ``Find the sum of all the even-valued terms in the sequence which do not exceed 89.`` () =     
    Given 89I
      |> When solving SumOfEvenValuedTermsInTheFibonacciSequence
      |> It should equal 44I
      |> Verify
    
[<Scenario>]
let ``Find the sum of all the even-valued terms in the sequence which do not exceed four million.`` () =   
    Given 4000000I
      |> When solving SumOfEvenValuedTermsInTheFibonacciSequence
      |> It should equal 4613732I
      |> Verify