{-
(+) :: Num a => a -> a -> a

sum :: Num p => [p] -> p -- Works only with instances of Num type classes
sum [] = 0
sum (x:xs) = x + sum xs
-}

data Temperatur = C Float | F Float

instance Eq Temperature where
    (==) (C n) (C m) = n == m
    (==) (F n) (F m) = n == m
    (==) (C c) (F f) = (1.8*c + 32) == f
    (==) (F f) (C c) = (1.8*c + 32) == f

data Temperature = C Float | F Float
    deriving (Show, Eq) -- Make compaler do it for you. Do the same but partly, only firs two from previous.

main :: IO()
main = do
    print()
