open System.IO

let lines =
    File.ReadLines("day03-binary-diagnostic/input.txt")

let reports =
    lines |> Seq.map (fun n -> n.ToCharArray())

let findMetric reports cmpFunc =
    reports
    |> Seq.transpose
    |> Seq.map (Seq.countBy id)
    |> Seq.map (cmpFunc (fun (char, count) -> count))
    |> Seq.map fst
    |> Seq.toArray
    |> (fun chars -> System.Convert.ToInt32(new System.String(chars), 2))

let gammaRate reports = findMetric reports Seq.maxBy

let eposilonRate reports = findMetric reports Seq.minBy

let result1 = gammaRate reports * eposilonRate reports
