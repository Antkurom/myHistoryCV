-- Initionalization of the function inc that increasse a number that this function get as an argument
inc a = a+1

-- Description of an function for compiler
myMap :: (a->b) -> [a] -> [b]
myMap _ [] = []
myMap f (x:xs) = f x : myMap f xs

main :: IO()
main = do
    print(myMap inc [0, 5, 7, 13, 25, 36, 100, 999])
