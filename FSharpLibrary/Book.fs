namespace Library

open System

type Book(title: string, author: string, genre: string) =
    member val Title = title with get, set
    member val Author = author with get, set
    member val Genre = genre with get, set
    member val IsBorrowed = false with get, set
    member val BorrowDate: DateTime option = None with get, set

     // Method to borrow a book
    member this.Borrow() =
        if not this.IsBorrowed then
            this.IsBorrowed <- true
            this.BorrowDate <- Some(DateTime.Now)
            true
        else
            false

    // Method to return a book
    member this.Return() =
        if this.IsBorrowed then
            this.IsBorrowed <- false
            this.BorrowDate <- None
            true
        else
            false

    // Method to check the availability of the book
    member this.IsAvailable() = not this.IsBorrowed

