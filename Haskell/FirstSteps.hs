in_range :: Integer -> Integer -> Integer -> Bool
in_range min max x = x >= min && x <= max

into_range :: Integer -> Integer -> Integer -> Bool
into_range min max x =
    let in_lower_bound = min <= x
        in_upper_bound = max >= x
    in
    in_lower_bound && in_upper_bound

intoRange :: Integer -> Integer -> Integer -> Bool
intoRange min max x = ilb && iub
-- if ilb then iub else False
 where
  ilb = min <= x
  iub = max >= x


add :: Integer -> Integer -> Integer
add a b = a+b


--fac n = 
-- if n <= 1 then 1
-- else
--  n * fac (n-1)

fac n
 | n <= 1    = 1
 | otherwise = n * fac (n-1)


--fac n = aux n 1
-- where
--  aux n acc
--   | n <= 1    = acc
--   | otherwise = aux (n-1) (n*acc)

app :: (a ->b) -> a -> b
app f x = f x

--add1 :: Int -> Int
--add1 x = x+1

add1 = (\x -> x+1)
--(\x y z -> x+y+z) 1 2 3 => 6


--map :: (a -> b) -> [a] -> [b]
--map (\x -> x+1)
--map (\(x,y) -> x+y) [(1,2),(3,4),(5,6)] => [3,7,11]

--filter :: (a -> Bool) -> [a] -> [a]
--filter(\x -> x>2) [1,2,3,4,5) => [3,4,5]        example
--filter(\(x,y) -> x\=y) [(1,2),(2,2)] => [(1,2)] example


--add:: Int -> Int -> Int
--add x y = x+y        different implementation
--add x = (\y -> x+y)  different implementation
--add = (\x -> (\y -> x+y))

--add 1 :: Int -> Int => (\y -> 1+y)

--descSort = reverse . sort           equvalent
--descSort = (\x -> reverse (sort x)) equvalent
--descSort x = reverse (sort x)       equvalent

map2D :: (a -> b) -> [[a]] -> [[b]]
map2D = map . map

--map2D = (\f1 xs -> map f1 xs) . (\f2 ys -> map f2 ys)
{-
map2D = (\x -> (\f1 xs -> map f1 xs) ((\f2 ys -> map f2 ys) x)

map2D x = (\f1 xs -> map f1 xs) ((\f2 ys -> map f2 ys) x)

map2D x = (\f1 xs -> map f1 xs) (\ys -> map x ys)

map2D x = (\xs -> map (\ys -> map x ys) xs)

map2D f xs = map (\ys -> map f ys) xs
-}
--they are all the same, and show why dot (composition is working as dimentioal mapping)

{-
($) :: (a -> b) -> a -> b
f xs = map (\x -> x+1) (filter (\x -> x>1) xs)
f xs = map (\x -> x+1) $ filter (\x -> x>1) xs
-}

--foldr :: (a -> b -> b) -> b -> [a] -> b regular folding
--foldr (\elem acc -> <term>) <start_acc> <list>
--foldl (\acc elem -> <term>) <start_acc> <list>

count e = foldr (\x acc -> if x==e then acc+1 else acc) 0
isAll e = foldr (\x acc -> x==e && acc) True

length = foldr (\x -> (+) 1) 0
map f = foldr ((:) . f) []

main :: IO ()
main = do
--    print (in_range 0 5 3)
--    print (into_range 0 5 3)
--    print (intoRange 0 5 3)
--    print (add 10 30)
--    print (10 `add` 30)
--    print (fac 5)
--    print (app add1 1)
--    print (app (\x -> x+1) 1)
  print (foldr (+) 0 [1,2,3,4,5])
