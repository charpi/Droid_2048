namespace test_lib
open System
open NUnit.Framework
open Droid_2048

[<TestFixture>]
type Test() = 

    [<Test>]
    member x.TestCase() =
        let expected = {GameGrid.score = 0; GameGrid.grid =  [[0; 0; 0; 0]; [4; 0; 0; 0]; [0; 0; 0; 0]; [0; 0; 0; 0]]} 
        let start = {GameGrid.score = 0; GameGrid.grid =  [[0; 0; 0; 0]; [2; 2; 0; 0]; [0; 0; 0; 0]; [0; 0; 0; 0]]} 
        Assert.Equals(expected, start)
        
