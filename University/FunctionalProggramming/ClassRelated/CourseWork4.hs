data Sheep = Sheep{firstName :: String, fatherName :: String, motherName :: String} deriving (Show)

father :: Sheep -> Sheep -> Maybe Sheep
father (Sheep _ f _ ) fAther@(Sheep n _ _) = if f == n then Just fAther else Nothing 

mother :: Sheep -> Sheep -> Maybe Sheep
mother (Sheep _ _ m) mOther@(Sheep n _ _) = if m == n then Just mOther else Nothing 

main :: IO()
main = do 
    let child = Sheep {firstName = "Ab", fatherName = "AA", motherName = "aA"}
    let fatheR = Sheep {firstName = "AA", fatherName = "BB", motherName = "bB"}
    print(show $ father child fatheR)
    print(show $ mother child fatheR)
