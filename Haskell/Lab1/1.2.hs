import Data.Char

toAlpha :: String -> String
toAlpha (x:xs) = toUpper x : xs

invertedNames :: [String] -> [String]
invertedNames [] = []
invertedNames (x:xs) = if null x then "" : invertedNames xs
  else toAlpha (map toLower (reverse x)) : invertedNames xs


main :: IO()
main = do
  print(invertedNames ["", "Ksenija", "Kris", "Andrew", "Mateo"])
