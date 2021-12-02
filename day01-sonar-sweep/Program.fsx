open System.IO

let depths =
    File.ReadLines("day01-sonar-sweep/input.txt")
    |> Seq.map int

let result1 =
    depths
    |> Seq.pairwise
    |> Seq.filter (fun (a, b) -> b > a)
    |> Seq.length

let result2 =
    depths
    |> Seq.windowed 3
    |> Seq.map (fun window -> Seq.reduce (+) window)
    |> Seq.pairwise
    |> Seq.filter (fun (a, b) -> b > a)
    |> Seq.length
