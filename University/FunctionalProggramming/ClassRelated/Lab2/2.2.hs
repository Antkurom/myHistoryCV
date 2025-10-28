runLengthEncoding :: [Int] -> [(Int, Int)]
runLengthEncoding x:xs = 

countNumbers :: [Int] -> Int -> Int -> (Int, [Int])
countNumbers x:xs y acc = if x==y then 
    countNumbers xs y acc+1
    else (acc, x:xs)

main :: IO()
main = do 
    print("Hi")
