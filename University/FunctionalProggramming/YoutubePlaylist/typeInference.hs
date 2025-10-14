-- add x y z = (x + y) : z  initioal funtion and we want to describe it as generic as possible
-- Step 1: arguments
{-
x :: a
y :: b
z :: c
-}
-- Step 2: all operations
{-
(+) :: (Num d) => d -> d -> d
(:) :: e -> [e] -> [e]
-}
-- Step 3: all sub expressions
{-
from (x+y) derive a = d and b = d
from (x+y) : z derive [e] = c and d = e
-}
-- Step 4: Simplifying
{-
x :: d
y :: d
z :: [e]
z :: [d]
-}
-- Final result
add :: (Num d) => d -> d -> [d] -> [d]
add x y z = (x + y) : z
{-
(+) :: (Num d) => d -> d -> d
(:) :: e -> [e] -> [e]
x :: d
y :: d
z :: [e]
z :: [d]
-}
{-
f = reverse . sort

reverse :: [a] -> [a]
(.) :: (c -> d) -> (b -> c) -> b -> d
sort :: Ord e => [e] -> [e]
from reverse . sort derive
    c = [a], d = [a], b = [e], c = [e]
==> a = e
==> f :: Ord a => [a] -> [a]
-}

{-
f x = x : x

x :: a
(:) :: b -> [b] -> [b]

from(x:x) derive a = b and a = [b] ==> b = [b] Type error!
-}
