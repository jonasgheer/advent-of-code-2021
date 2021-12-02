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

let followCmd1 (d, h) (cmd) =
    match cmd with
    | Forward x -> (d, h + x)
    | Down x -> (d + x, h)
    | Up x -> (d - x, h)

let followCmd2 (d, h, a) (cmd) =
    match cmd with
    | Forward x -> (d + a * x, h + x, a)
    | Down x -> (d, h, a + x)
    | Up x -> (d, h, a - x)

let result1 =
    commands
    |> Seq.fold followCmd1 (0, 0)
    |> (fun (d, h) -> d * h)

let result2 =
    commands
    |> Seq.fold followCmd2 (0, 0, 0)
    |> (fun (d, h, _) -> d * h)
