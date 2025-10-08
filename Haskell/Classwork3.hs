factorial :: Int -> Int
factorial n = if n <= 1 then 1 else n * factorial (n-1)

fac :: Int -> Int
fac n = fact_iter 1 1 n

fact_iter :: Int -> Int -> Int -> Int
fact_iter product counter max_count = if counter > max_count then product else fact_iter (counter * product) (counter+1) max_count

main :: IO()
main = do
  print (factorial 6)
  print (fac 6)
