tan_cf_loop :: Float -> Float -> Float -> Float
tan_cf_loop x k loop = 
 if (loop == k) then ((2*loop)-1)-(x*x)
 else ((2*loop)-1)-(x*x)/(tan_cf_loop x k (loop+1))


tan_cf :: Float -> Float -> Float
tan_cf x k
 | x==0 = 0.0
 | k<=0 = x
 | otherwise = x/(tan_cf_loop x k 1)


main :: IO() 
main = do
  print(tan_cf 5.0 5)

