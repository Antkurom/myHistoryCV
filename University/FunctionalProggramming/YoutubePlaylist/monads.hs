(>>=) :: Monad m => m a -> (a -> m b) -> m b

--Just 1 >>= (\x -> Just x) ==> Just 1
--Nothing >>= (\x -> Just x) ==> Nothing

maybeadd :: Num b => Maybe b -> b -> Maybe b
maybeadd mx y = mx >>= (\x -> Just $ x+y)
-- maybeadd Nothing 1 ==> Nothing   maybeadd (Just 1) 1 ==> Just 2

maybeadd :: Num b => Maybe b -> Maybe b -> Maybe b
maybeadd mx my = mx >>= (\x -> my >>= (\y -> Just $ x+y))
-- maybeadd Nothing (Just 1) ==> Nothing

monadd :: (Monad m, Num b) => m b -> m b -> m b
monadd mx my = mx >>= (\x -> my >>= (\y -> return $ x+y)) 
-- m >>= (\x -> ...) is equvalent to 
-- do 
--    x <- m
--    ...

monadd mx my = do
    x <- mx
    y <- my
    return $ x+y

instance Monad Maybe where
    m >>= f = case m of 
                Nothing -> Nothing
                Just x -> f x
    return v = Just v

(>>) :: Monad m => m a -> m b -> m b
m >> n = m >>= \_ -> n
-- used to check for errors but ignore the output of or value of it

-- Monad Laws
-- Left identity
-- return a >>= k = k a
-- Right identity
-- m >>= return = m
-- Associativity
-- m >>= (\x -> k x >>= h) = (m >>= k) >>= h 
