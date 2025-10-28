module Main where

import Network.HTTP.Simple
import qualified Data.ByteString.Lazy as LBS

main :: IO ()
main = do
putStrLn "Testing API call..."


    request <- parseRequest "GET https://httpbin.org/get"
    response <- httpLBS request

    putStrLn $ "Status: " ++ show (getResponseStatusCode response)
    LBS.putStrLn $ getResponseBody response
