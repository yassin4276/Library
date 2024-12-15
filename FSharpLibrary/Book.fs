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
            
   // Create a new instance of Book with updated Returned status
    static member Return(book: Book) =
        if book.IsBorrowed then
            Some { book with IsBorrowed = false; BorrowDate = None }
        else
            None

    // Check if the book is available
    static member IsAvailable(book: Book) = 
        not book.IsBorrowed

