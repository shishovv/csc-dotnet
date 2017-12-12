namespace HW7

module Main = 

    let fib n = 
        let rec helper i prevprev prev = 
            if i = n then prev 
            else helper (i + 1) prev (prevprev + prev) 
        let fib0 = 1
        let fib1 = 1
        match n with
        | 0 -> fib0
        | 1 -> fib1
        | _ -> helper 1 fib0 fib1

    let reverse lst = 
        let rec helper acc l = 
            match l with
            | [] -> acc
            | x::xs  -> helper (x::acc) xs
        helper [] lst

    let rec merge = function
        | ([], l)                -> l
        | (l, [])                -> l
        | ((x1::xs1), (x2::xs2)) -> 
            if x1 <= x2 then x1::(merge (xs1, (x2::xs2)))
            else x2::(merge ((x1::xs1), xs2))

    let rec mergesort = function
        | []      -> []
        | (h::[]) -> [h]
        | lst     -> 
            let l = Seq.toList <| Seq.take (Seq.length lst / 2) lst
            let r = Seq.toList <| Seq.skip (Seq.length l) lst
            merge ((mergesort l), (mergesort r))

    type Expr =
        | Const of int
        | Add of Expr * Expr
        | Sub of Expr * Expr
        | Mul of Expr * Expr
        | Div of Expr * Expr
    
    let rec calc = function
        | Const c -> c
        | Add (a, b) -> (calc a) + (calc b)
        | Sub (a, b) -> (calc a) - (calc b)
        | Mul (a, b) -> (calc a) * (calc b)
        | Div (a, b) -> (calc a) / (calc b)

    let isPrime n = 
        let rec helper i k = 
            match i with
            | i when i * i > k -> true
            | i when k % i = 0 -> false
            | i                -> helper (i + 1) k
        helper 2 n

    let primes = 
        let rec helper cur = seq {
            match cur with
                | c when isPrime c -> 
                    yield c
                    yield! helper (c + 1)
                | _ -> yield! helper (cur + 1)
        }
        helper 2
