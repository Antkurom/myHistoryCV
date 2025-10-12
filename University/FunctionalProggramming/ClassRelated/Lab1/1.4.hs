tupleMapping :: (a, b) -> (c, d) -> (a, d)
tupleMapping (a,b) (c,d) = (a, d)

main :: IO()
main = do
    putStrLn "There will be four inputs: (i1, i2), (i3, i4)" 
    print( "(i1, i2), (i3, i4). Enter first value:")
    i1 <- getLine
    print( "("++i1++", i2), (i3, i4). Enter second value:")
    i2 <- getLine
    print( "("++i1++", "++i2++"), (i3, i4). Enter third value:")
    i3 <- getLine
    print( "("++i1++", "++i2++"), ("++i3++", i4). Enter fourth value:")
    i4 <- getLine
    if (i1 == "" || i2 == "" || i3 == "" || i4 == "") then print("Input values can't be empty, next time fill them up")
        else print (tupleMapping (i1, i2) (i3, i4))     
