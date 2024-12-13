namespace Library

open System

type Book(title: string, author: string, genre: string) =
    member val Title = title with get, set
    member val Author = author with get, set
    member val Genre = genre with get, set