open System.IO
open System.Text.RegularExpressions

type Square =
    { Value: int
      Marked: bool
      X: int
      Y: int }

type Board = Square [] seq

let lines =
    File.ReadLines("day04-giant-squid/input.txt")

let boardSize = 5

let numbers =
    lines
    |> Seq.head
    |> (fun s -> s.Split(','))
    |> Seq.map int

let makeBoard board : Board =
    Seq.mapi
        (fun i row ->
            Array.mapi
                (fun j square ->
                    { Value = square |> int
                      X = i
                      Y = j
                      Marked = false })
                row)
        board

let boards =
    lines
    |> Seq.skip 1
    |> Seq.filter (fun s -> s <> "")
    |> Seq.map (fun s -> Regex.Split(s.Trim(), "\s+"))
    |> Seq.chunkBySize boardSize
    |> Seq.map makeBoard

let markBoard board num =
    board
    |> Seq.concat
    |> Seq.map (fun square ->
        if square.Value = num then
            { square with Marked = true }
        else
            square)
    |> Seq.chunkBySize boardSize

let markBoards (boards: seq<Board>) (num: int) : seq<Board> =
    boards
    |> Seq.map (fun board -> markBoard board num)

let rowIsWin row = Array.forall (fun s -> s.Marked) row

let boardIsWin (board: Board) =
    Seq.exists
        rowIsWin
        (Seq.concat [ board
                      Array.transpose board ])

let rec runGame (boards: seq<Board>) numbers =
    let n = Seq.head numbers
    let markedBoards = markBoards boards n

    match Seq.tryFind boardIsWin markedBoards with
    | Some winningBoard -> winningBoard, n
    | None -> runGame markedBoards (Seq.skip 1 numbers)

let winningBoard = runGame boards numbers

let sumOfUnmarked board =
    board
    |> Seq.concat
    |> Seq.filter (fun s -> s.Marked = false)
    |> Seq.sumBy (fun s -> s.Value)

let result1 =
    sumOfUnmarked (fst winningBoard)
    * (snd winningBoard)

let rec findLastWinner (boards: seq<Board>) numbers =
    let n = Seq.head numbers
    let markedBoards = markBoards boards n

    let notYetWon =
        markedBoards |> Seq.filter (boardIsWin >> not)

    let remainingNumbers = Seq.skip 1 numbers

    match Seq.tryExactlyOne notYetWon with
    | Some lastBoard -> runGame [ lastBoard ] remainingNumbers
    | None -> findLastWinner notYetWon remainingNumbers

let lastWinner = findLastWinner boards numbers

let result2 =
    sumOfUnmarked (fst lastWinner) * (snd lastWinner)
