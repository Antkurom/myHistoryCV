-- String - Name; Int - Age
data Person = Person String Int

data Person = Person {name :: String, age :: Int}

greet :: Person -> [Char]
greet person = "Hi " ++ name person

greet :: Person -> [Char]
greet (Person n _) = "Hi " ++ n

data Point = 
    D2 {x :: Int, y :: Int}
  | D3 {x :: Int, y :: Int, z :: Int}

{-
x (D2 1 2) => 1
x (D3 1 2 3) => 1
z (D2 1 2) => *** Exception: No match in record selector z
-}
