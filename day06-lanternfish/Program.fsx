open System.IO

let fish =
    File
        .ReadAllText("day06-lanternfish/input.txt")
        .Split(',')
    |> Seq.map int
    |> Seq.toList

let rec cycleFish fish days =
    let newFish =
        fish
        |> Seq.filter (fun f -> f = 0)
        |> Seq.length
        |> fun count -> List.init count (fun _ -> 8)

    let fish =
        fish
        |> List.map (fun f -> if f = 0 then 6 else f - 1)

    if days = 1 then
        fish @ newFish
    else
        cycleFish (fish @ newFish) (days - 1)

let result1 = cycleFish fish 80 |> Seq.length
