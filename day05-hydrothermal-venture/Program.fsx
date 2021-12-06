open System.IO

type Point = int * int

type Line = { Start: Point; End: Point }

let commaPairToPoint (str: string) = 
    let str = str.Split(',')
    int str[0], int str[1] 

let lines =
    File.ReadLines("day05-hydrothermal-venture/input.txt")
    |> Seq.map (fun s -> 
        let str = s.Split("->")
        {Start = commaPairToPoint str[0]; End = commaPairToPoint str[1]})
    
let isHorizontal line = fst line.Start = fst line.End 

let isVertical line = snd line.Start = snd line.End   

let isHorizontalOrVertical line =  isHorizontal line || isVertical line

let generatePoints line = 
    let x1, y1 = line.Start
    let x2, y2 = line.End
    let inc a b = if a = b then (fun p -> p+0)
                  elif a < b then (fun p -> p+1)
                  else (fun p -> p-1)
    let incX = inc x1 x2
    let incY = inc y1 y2

    let rec generatePoints points point =
        let x, y = point
        let newPoint = incX x, incY y
        let points = point :: points
        if point = line.End then points
        else generatePoints points newPoint

    generatePoints [] line.Start

let findPointsWithOverlap lines = 
    lines
    |> Seq.map generatePoints 
    |> Seq.concat 
    |> Seq.countBy id 
    |> Seq.filter (fun point -> snd point >= 2) 
    |> Seq.length

let result1 = 
    lines 
    |> Seq.filter isHorizontalOrVertical 
    |> findPointsWithOverlap

let result2 = 
    lines 
    |> findPointsWithOverlap