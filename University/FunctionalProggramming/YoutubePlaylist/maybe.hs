data Maybe a = Nothing | Just a

safediv :: Int a => a -> a -> Maybe a
safediv a b = if b == 0 then Nothing else Just $ div a b

-- Data.Maybe
-- isJust :: Maybe a -> Bool
-- isNothing :: Maybe a -> Bool
-- fromJust :: Maybe a -> a
--
-- fromMaybe :: a -> Maybe a -> a
-- fromMaybe 3 (Nothing) => 3
-- formMaybe 3 (Just 4) => 4

main :: IO()
main = do
    print ()

{-Exemple of console that will not be closed until you type quit
main :: IO()
main = do
    i <- getLine
    if i /= "quit" then do
        putStrLn ("Input: " ++ i)
        main
    else
        return ()
-}
