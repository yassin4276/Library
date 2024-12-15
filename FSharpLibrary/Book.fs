namespace Library

open System

type Book = 
    { Title: string
      Author: string
      Genre: string
      IsBorrowed: bool
      BorrowDate: DateTime option }

   // Create a new instance of Book with updated Borrowed status
    static member Borrow(book: Book) =
        if not book.IsBorrowed then
            Some { book with IsBorrowed = true; BorrowDate = Some(DateTime.Now) }
        else
            None
            
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

