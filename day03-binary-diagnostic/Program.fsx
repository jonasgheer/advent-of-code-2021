open System.IO

let lines =
    File.ReadLines("day03-binary-diagnostic/input.txt")

let reports =
    lines
    |> Seq.map (fun n -> n.ToCharArray())
    |> Seq.toList

let charArrayToInt (chars: char []) =
    System.Convert.ToInt32(new System.String(chars), 2)

let findMetric reports cmpFunc =
    reports
    |> Seq.transpose
    |> Seq.map (Seq.countBy id)
    |> Seq.map (cmpFunc (fun (_, count) -> count))
    |> Seq.map fst
    |> Seq.toArray
    |> charArrayToInt

let gammaRate reports = findMetric reports Seq.maxBy

let eposilonRate reports = findMetric reports Seq.minBy

let result1 = gammaRate reports * eposilonRate reports

let mostCommonAtIndex reports index =
    reports
    |> Seq.transpose
    |> Seq.item index
    |> Seq.countBy id
    |> Seq.sortByDescending id // to make sure '1' is returned if count is equal
    |> Seq.maxBy (fun (_, count) -> count)
    |> fst

let leastCommonAtIndex reports index =
    reports
    |> Seq.transpose
    |> Seq.item index
    |> Seq.countBy id
    |> Seq.sortBy id // to make sure '1' is returned if count is equal
    |> Seq.minBy (fun (_, count) -> count)
    |> fst

let rec filterDownReports remainingReports index x cmpFunc =
    match List.filter (fun r -> (Seq.item index r) = x) remainingReports with
    | [ report ] -> [ report ]
    | reports ->
        let newIndex = index + 1
        filterDownReports reports newIndex (cmpFunc reports newIndex) cmpFunc

let oxygenGeneratorRating =
    filterDownReports reports 0 (mostCommonAtIndex reports 0) mostCommonAtIndex
    |> List.head
    |> charArrayToInt

let co2ScrubberRating =
    filterDownReports reports 0 (leastCommonAtIndex reports 0) leastCommonAtIndex
    |> List.head
    |> charArrayToInt

let result2 =
    oxygenGeneratorRating * co2ScrubberRating
