runLengthEncoding :: [Int] -> [(Int, Int)]
runLengthEncoding (x:xs) = countNumbers xs x 1

countNumbers :: [Int] -> Int -> Int -> [(Int, Int)]
countNumbers [] y acc = (acc, y):[]
countNumbers (x:xs) y acc = if x==y 
    then countNumbers xs y (acc+1)
    else (acc, y):countNumbers xs x 1

secondRunLengthEncoding :: [Int] -> [[Int]]
secondRunLengthEncoding (x:xs) = correctFormatting xs x [x]

correctFormatting :: [Int] -> Int -> [Int] -> [[Int]]
correctFormatting [] _ ys = ys:[]
correctFormatting (x:xs) y ys = if x==y
    then correctFormatting xs y (x:ys)
    else ys:correctFormatting xs x [x]


main :: IO()
main = do 
    print(runLengthEncoding [1,2,2,3,4,5,4,5])
    print(secondRunLengthEncoding [1,2,2,3,4,5,4,5,5])
