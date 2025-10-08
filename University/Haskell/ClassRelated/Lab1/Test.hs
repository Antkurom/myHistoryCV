isVowel :: Char -> Bool
isVowel c = foldr(\x acc -> (x==c) || acc ) False "aeuioAEUIO"

rev :: [a] -> [a]
rev = foldl (\acc x -> x : acc) []


main :: IO()
main = do
  print (func 'a')
