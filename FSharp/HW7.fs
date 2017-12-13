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

    let rec merge lst1 lst2 = 
        match lst1, lst2 with
        | [], l                -> l
        | l, []                -> l
        | (x1::xs1), (x2::xs2) -> 
            if x1 <= x2 then x1::(merge xs1 lst2)
            else x2::(merge lst1 xs2)

    let rec mergesort = function
        | []       -> []
        | [h] as l -> l
        | lst      -> 
            let l = List.take (List.length lst / 2) lst
            let r = List.skip (List.length l) lst
            merge (mergesort l) (mergesort r)

    type Expr =
        | Con of int
        | Add of Expr * Expr
        | Sub of Expr * Expr
        | Mul of Expr * Expr
        | Div of Expr * Expr
    
    let rec calc = function
        | Con c -> c
        | Add (a, b) -> (calc a) + (calc b)
        | Sub (a, b) -> (calc a) - (calc b)
        | Mul (a, b) -> (calc a) * (calc b)
        | Div (a, b) -> (calc a) / (calc b)

    let isPrime n = 
        let d = [2 .. float >> sqrt >> int <| n]
        List.isEmpty <| List.skipWhile (fun e -> n % e <> 0) d

    let primes = Seq.filter isPrime <| Seq.initInfinite (fun e -> e + 1)
