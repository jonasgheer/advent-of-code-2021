open System.IO

// Todo: find a solution without mutible vars
let result1 =
    let mutable depth = 0
    let mutable horizontal = 0

    for line in File.ReadLines("day02-dive/input.txt") do
        match line.Split(' ') with
        | [| "forward"; x |] -> horizontal <- horizontal + (x |> int)
        | [| "down"; x |] -> depth <- depth + (x |> int)
        | [| "up"; x |] -> depth <- depth - (x |> int)
        | _ -> failwith "invalid command string"

    depth * horizontal
