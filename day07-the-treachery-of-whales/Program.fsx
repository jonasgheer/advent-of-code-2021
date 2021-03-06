open System.IO

let positions =
    File
        .ReadAllText("day07-the-treachery-of-whales/input.txt")
        .Split(',')
    |> Seq.map int

let possiblePositions =
    [ Seq.min positions .. Seq.max positions ]

let calculateCost positions target =
    positions
    |> Seq.map (fun p -> abs (target - p))
    |> Seq.reduce (+)

let result1 =
    possiblePositions
    |> Seq.map (fun p -> calculateCost positions p)
    |> Seq.min

let calculateNewCost positions target =
    positions
    |> Seq.map (fun p -> Seq.fold (+) 0 [ 1 .. abs (target - p) ])
    |> Seq.reduce (+)

let result2 =
    possiblePositions
    |> Seq.map (fun p -> calculateNewCost positions p)
    |> Seq.min
