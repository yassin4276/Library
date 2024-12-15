namespace LibraryManagement

open System
open System.IO
open Newtonsoft.Json
open Library

type BookManagement() =
    let filePath = "books.json"

    // Helper to read books from file
    member this.LoadBooksFromFile () =
        try
            if File.Exists(filePath) then
                let json = File.ReadAllText(filePath)
                JsonConvert.DeserializeObject<Book list>(json)
            else
                []  // Return an empty list if the file doesn't exist
        with
        | ex -> 
            System.Windows.Forms.MessageBox.Show($"Error loading books: {ex.Message}") |> ignore
            []  // Return an empty list on error

    // Helper to save books to file
    member this.SaveBooksToFile (books: Book list) =
        try
            let json = JsonConvert.SerializeObject(books, Formatting.Indented)
            File.WriteAllText(filePath, json)
        with
        | ex -> 
            System.Windows.Forms.MessageBox.Show($"Error saving books: {ex.Message}") |> ignore

    // Add a new book to the collection
    member this.AddBook(title: string, author: string, genre: string) =
        let book = { Title = title; Author = author; Genre = genre; IsBorrowed = false; BorrowDate = None }
        let books = this.LoadBooksFromFile()
        this.SaveBooksToFile (book :: books)
        
        // Borrow a book by title
    member this.BorrowBook(title: string) =
        let books = this.LoadBooksFromFile()
        match books |> List.tryFind (fun book -> book.Title = title) with
        | Some(book) when Book.IsAvailable(book) -> 
            let updatedBook = Book.Borrow(book)
            match updatedBook with
            | Some newBook -> 
                this.SaveBooksToFile (books |> List.map (fun b -> if b = book then newBook else b))
                System.Windows.Forms.MessageBox.Show($"Book '{newBook.Title}' borrowed successfully!") |> ignore
            | None -> 
                System.Windows.Forms.MessageBox.Show($"Book '{book.Title}' is already borrowed.") |> ignore
        | Some(_) -> 
            System.Windows.Forms.MessageBox.Show($"Book '{title}' is already borrowed.") |> ignore
        | None -> 
            System.Windows.Forms.MessageBox.Show("Book not found.") |> ignore
            
           // Search a book by partial title (case-insensitive)
    member this.SearchBookByTitle(searchTerm: string) =
        books |> List.filter (fun book -> 
            book.Title.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)

             // Return a borrowed book
    member this.ReturnBook(title: string) =
        match this.SearchBookByTitle(title) with
        | [book] when not (book.IsAvailable()) -> 
            let success = book.Return()
            if success then
                this.SaveBooksToFile()
                System.Windows.Forms.MessageBox.Show($"Book '{book.Title}' returned successfully!") |> ignore
            else
                System.Windows.Forms.MessageBox.Show($"Book '{book.Title}' is not borrowed.") |> ignore
        | [book] -> 
            System.Windows.Forms.MessageBox.Show($"Book '{book.Title}' is not borrowed.") |> ignore
        | _ -> 
            System.Windows.Forms.MessageBox.Show("Book not found.") |> ignore

            
     // Display all books
    member this.DisplayBooks() =
        let books = this.LoadBooksFromFile()
        let booksInfo = 
            books |> List.map (fun book -> 
                $"{book.Title} by {book.Author} ({book.Genre}) - " + 
                (if Book.IsAvailable(book) then "Available" else "Borrowed"))
            |> String.concat "\n"
        System.Windows.Forms.MessageBox.Show(booksInfo) |> ignore
        
        // View borrowed books
    member this.ViewBorrowedBooks() =
        let books = this.LoadBooksFromFile()
        let borrowedBooks = 
            books |> List.filter (fun book -> not (Book.IsAvailable(book)))
                |> List.map (fun book -> $"{book.Title} by {book.Author} (Borrowed on {book.BorrowDate.Value})")
                |> String.concat "\n"
        if borrowedBooks <> "" then
            System.Windows.Forms.MessageBox.Show($"Borrowed Books:\n{borrowedBooks}") |> ignore
        else
            System.Windows.Forms.MessageBox.Show("No books borrowed currently.") |> ignore



