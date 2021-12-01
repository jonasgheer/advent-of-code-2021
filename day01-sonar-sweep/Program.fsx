open System.IO

let result =
    File.ReadLines("day01-sonar-sweep/input.txt")
    |> Seq.map int
    |> Seq.pairwise
    |> Seq.map (fun (a, b) -> b > a)
    |> Seq.filter id
    |> Seq.length
