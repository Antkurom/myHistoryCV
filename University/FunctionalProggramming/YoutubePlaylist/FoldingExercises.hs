rev :: [a] -> [a]
rev = foldl(\acc x -> x : acc) []

prefixes :: [a] -> [[a]]
prefixes = foldr(\x acc -> [x]:(map ((:) x) acc)) []

langrange :: [(Float, Float)] -> Float -> Float
langrange xs x = foldl (\acc (xj,y) -> acc + (y * l xj)) 0 xs
  where
    l xj = foldl(\acc (xk,_) -> if xj==xk then acc else acc * ((x-xk)/(xj-xk))) 1 xs

data Trie a = Leaf a | Node a [Trie a]
foldtrie :: (b -> a -> b) -> b -> Trie a -> b
foldtrie f acc (Leaf x) = f acc x
foldtrie f acc (Node x xs) = foldl f' (f acc x) xs
  where
    f' acc t = foldtrie f acc t

main :: IO()
main = do 
--    print(rev [1,2,3,4,5,6,7])
    print(prefixes [1,2,3,4,5,6,7])
