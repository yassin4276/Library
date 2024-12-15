namespace Library

open System

type Book = 
    { Title: string
      Author: string
      Genre: string
      IsBorrowed: bool
      BorrowDate: DateTime option }

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

