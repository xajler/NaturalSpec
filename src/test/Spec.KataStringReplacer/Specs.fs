﻿module StringReplacer.Specs

open NaturalSpec

open Model

let replacing replacements text =
    printMethod replacements
    let dict = replacements |> Map.ofSeq
    replace dict text


[<Scenario>]     
let ``Should yield empty text when empty text is provided`` () =   
    Given ""
      |> When replacing []
      |> It should equal ""
      |> Verify

[<Scenario>]     
let ``Should yield the given text when no pattern is included`` () =   
    Given "some text"
      |> When replacing []
      |> It should equal "some text"
      |> Verify

[<Scenario>]     
let ``Should replace key with value when key was found`` () =   
    Given "hi $who$"
      |> When replacing ["who","bingo"]
      |> It should equal "hi bingo"
      |> Verify
        
[<Scenario>]     
let ``Should replace multiple keys with values when found`` () =   
    Given "$say$ $who$"
      |> When replacing ["who","bingo"; "say","hello"]
      |> It should equal "hello bingo"
      |> Verify

let searching_for_template text =
    printMethod ""
    findFirstPattern text

[<Scenario>]     
let ``Should find a single template`` () =   
    Given "hello $bingo$"
      |> When searching_for_template
      |> It should equal (Some "$bingo$")
      |> Verify

[<Scenario>]     
let ``Should not find template in string without $`` () =   
    Given "hello bingo"
      |> When searching_for_template
      |> It should equal None
      |> Verify

[<Scenario>]     
let ``Should not find template in string with only one $`` () =   
    Given "hello $bingo"
      |> When searching_for_template
      |> It should equal None
      |> Verify

[<Scenario>]     
let ``Should find a different first template`` () =   
    Given "hello $clowno$"
      |> When searching_for_template
      |> It should equal (Some "$clowno$")
      |> Verify


[<Scenario>]     
let ``Should find the first template in multiple`` () =   
    Given "hello $bingo$ $the$ $clowno$"
      |> When searching_for_template
      |> It should equal (Some "$bingo$")
      |> Verify

[<Scenario>]     
let ``Should remove placeholders when key was not found`` () =   
    Given "$say$ $me$"
      |> When replacing ["who","bingo"; "say","hello"]
      |> It should equal "hello "
      |> Verify

[<Scenario>]     
let ``Should remove multiple placeholders when key was not found`` () =   
    Given "$say$ $me$$really$ $who$"
      |> When replacing ["who","bingo"; "say","hello"]
      |> It should equal "hello  bingo"
      |> Verify

[<Scenario>]     
let ``Should replace a recursive template`` () =   
    Given "$say$ $who$ $recurse$"
      |> When replacing ["who","bingo"; "say","hello"; "recurse","$say$"]
      |> It should equal "hello bingo hello"
      |> Verify


[<Scenario>]     
let ``Should replace a full recursive template`` () =   
    Given "$say$ $who$ $recurse$"
      |> When replacing ["who","bingo"; "say","hello"; "recurse","$recurse$"]
      |> It should equal "hello bingo $recurse$"
      |> Verify
