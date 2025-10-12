numberOfSollutions :: Int -> Int -> Int -> Int
numberOfSollutions 0 m _ = if m == 0 then -1 else 1
numberOfSollutions k m n = let d = m*m+4*k*m*n in
                        if(d < 0) then 0 else if (d == 0) then 1
                        else 2

main :: IO()
main = do 
    print("There will be 3 inputs of k, m and n. For kx^2 + m(x-n) = 0.")
    putStr "Enter k: "
    input <- getLine
    let k = read input :: Int
    putStr "Enter m: "
    input <- getLine
    let m = read input :: Int
    putStr "Enter n: "
    input <- getLine
    let n = read input :: Int
    print("Final function: "++show k ++"x^2 + "++show m ++"(x-"++show n ++") = 0")
    let answer = numberOfSollutions k m n
    print("Number of sollutions: " ++ if (answer == -1) then "Infinite" else show answer)
