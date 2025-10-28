data Tree a = Branch a (Tree a) (Tree a) | Leaf deriving (Show,Eq)

isSearchTree :: Ord a => Tree a -> Bool
isSearchTree tree = checkTree tree Nothing Nothing

checkTree :: Ord a => Tree a -> Maybe a -> Maybe a -> Bool
checkTree Leaf _ _ = True
checkTree (Branch x left right) less more = withinRange x less more && checkTree left less (Just x) && checkTree right (Just x) more

withinRange :: Ord a => a -> Maybe a -> Maybe a -> Bool
withinRange value Nothing Nothing = True
withinRange value (Just newLess) Nothing = value > newLess
withinRange value Nothing (Just newMore) = value < newMore
withinRange value (Just newLess) (Just newMore) = value > newLess && value < newMore

insertTree :: Ord a => a -> Tree a -> Tree a 
insertTree e Leaf = Branch e Leaf Leaf
insertTree e (Branch x li re) 
 | e <= x = Branch x (insertTree e li) re
 | e > x = Branch x li (insertTree e re)

insertTreeBug1 :: Ord a => a -> Tree a -> Tree a 
insertTreeBug1 e Leaf = Branch e Leaf Leaf
insertTreeBug1 e (Branch x li re) 
 | e >= x = Branch x (insertTreeBug1 e li) re
 | e > x = Branch x li (insertTreeBug1 e re)

insertTreeBug2 :: Ord a => a -> Tree a -> Tree a 
insertTreeBug2 e Leaf = Branch e Leaf Leaf
insertTreeBug2 e (Branch x li re) 
 | e <= x = Branch x li (insertTreeBug2 e re)
 | e > x = Branch x (insertTreeBug2 e li) re

insertTreeBug3 :: Ord a => a -> Tree a -> Tree a 
insertTreeBug3 e Leaf = Branch e Leaf Leaf
insertTreeBug3 e (Branch x li re) 
 | e <= x = Branch x li re
 | e > x = Branch x li re 


main :: IO()
main = do
    let tree = Branch 10 (Branch 7 (Branch 4 (Branch 1 Leaf Leaf) (Branch 5 Leaf Leaf)) (Branch 8 Leaf (Branch 9 Leaf Leaf))) (Branch 25 Leaf Leaf)
    putStrLn("The original tree is " ++ show(isSearchTree(tree)))
    putStrLn("The tree after normal insert is " ++ show(isSearchTree(insertTree 2 tree)))
--    putStrLn("The tree after 1 bug insert is " ++ show(isSearchTree(insertTreeBug1 2 tree)))
    putStrLn("The tree after 2 bug insert is " ++ show(isSearchTree(insertTreeBug2 2 tree)))
    putStrLn("The tree after 3 bug insert is " ++ show(isSearchTree(insertTreeBug3 2 tree)))
