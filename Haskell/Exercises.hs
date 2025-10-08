elem1 :: (Eq a) => a -> [a] -> Bool
elem1 _ [] = False
elem1 e (x : xs) = (x==e) || (elem e xs)

nub :: (Eq a) => [a] -> [a]
nub [] = []
nub (x : xs)  
 | elem1 x xs = nub xs 
 | otherwise = x : nub xs

isAsc :: [Int] -> Bool
isAsc [] = True
isAsc [a] = True
isAsc (x:y:xs) =
     (x<=y) && isAsc (y:xs)

hasPath :: [(Int,Int)] -> Int -> Int -> Bool
hasPath [] x y = x == y
hasPath xs x y 
 | x == y = True
 | otherwise =
 let xs' = [(m,n) | (m,n)<-xs, m/=x] in
 or [hasPath xs' n y | (m,n)<-xs, m == x]


main :: IO()
main = do
--    print (elem1 7 [1, 2, 3, 4, 5, 6])
--    print (nub [1, 2, 3, 4, 5, 6, 6, 7, 1, 1])
--    print (isAsc [1, 2, 3, 4, 5, 5, 5, 5])
--    print (isAsc [1, 2, 5, 3])
    print (hasPath [(1,2), (2,1), (3,1), (2, 6), (3, 4), (4, 5), (2, 4), (7, 3)] 1 4)
    print (hasPath [(1,2), (2,1), (3,1), (2, 6), (3, 4), (4, 5), (2, 4), (7, 3)] 1 3)
