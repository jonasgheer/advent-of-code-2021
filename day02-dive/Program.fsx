open System.IO

type Command =
    | Forward of int
    | Down of int
    | Up of int

let makeCommand (cmdString: string) =
    match cmdString.Split(' ') with
    | [| "forward"; x |] -> Forward(x |> int)
    | [| "down"; x |] -> Down(x |> int)
    | [| "up"; x |] -> Up(x |> int)
    | _ -> failwith "invalid command string"

let commands =
    File.ReadLines("day02-dive/input.txt")
    |> Seq.map makeCommand

// Todo: find a solution without mutible vars

let result1 =
    let mutable depth = 0
    let mutable horizontal = 0

    for c in commands do
        match c with
        | Forward x -> horizontal <- horizontal + x
        | Down x -> depth <- depth + x
        | Up x -> depth <- depth - x

    depth * horizontal

let result2 =
    let mutable depth = 0
    let mutable horizontal = 0
    let mutable aim = 0

    for c in commands do
        match c with
        | Forward x ->
            horizontal <- horizontal + x
            depth <- depth + aim * x
        | Down x -> aim <- aim + x
        | Up x -> aim <- aim - x

    depth * horizontal
