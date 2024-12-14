namespace LibraryManagement

open System
open System.IO
open Newtonsoft.Json
open Library

type BookManagement() =
    let mutable books = []

    let filePath = "books.json"

    // Save books to file
    member this.SaveBooksToFile() =
        try
            let json = JsonConvert.SerializeObject(books, Formatting.Indented)
            File.WriteAllText(filePath, json)
        with
        | ex -> System.Windows.Forms.MessageBox.Show($"Error saving books: {ex.Message}") |> ignore

    // Load books from file
    member this.LoadBooksFromFile() =
        try
            if File.Exists(filePath) then
                let json = File.ReadAllText(filePath)
                books <- JsonConvert.DeserializeObject<Book list>(json)
            else
                System.Windows.Forms.MessageBox.Show("No saved data found, starting with an empty library.") |> ignore
        with
        | ex -> System.Windows.Forms.MessageBox.Show($"Error loading books: {ex.Message}") |> ignore

    // Add a new book
    member this.AddBook(title: string, author: string, genre: string) =
        let book = new Book(title, author, genre)
        books <- book :: books
        this.SaveBooksToFile()

        // Borrow a book
    member this.BorrowBook(title: string) =
        match this.SearchBookByTitle(title) with
        | [book] when book.IsAvailable() -> 
            let success = book.Borrow()
            if success then
                this.SaveBooksToFile()
                System.Windows.Forms.MessageBox.Show($"Book '{book.Title}' borrowed successfully!") |> ignore
            else
                System.Windows.Forms.MessageBox.Show($"Book '{book.Title}' is already borrowed.") |> ignore
        | [book] -> 
            System.Windows.Forms.MessageBox.Show($"Book '{book.Title}' is already borrowed.") |> ignore
        | _ -> 
            System.Windows.Forms.MessageBox.Show("Book not fou

         // Display all books
    member this.DisplayBooks() =
        let booksInfo = 
            books |> List.map (fun book -> 
                $"{book.Title} by {book.Author} ({book.Genre}) - " + 
                (if book.IsAvailable() then "Available" else "Borrowed"))
            |> String.concat "\n"
        System.Windows.Forms.MessageBox.Show(booksInfo) |> ignore

    // View all borrowed books
    member this.ViewBorrowedBooks() =
        let borrowedBooks = 
            books |> List.filter (fun book -> not (book.IsAvailable()))
                |> List.map (fun book -> $"{book.Title} by {book.Author} (Borrowed on {book.BorrowDate.Value})")
                |> String.concat "\n"
        if borrowedBooks <> "" then
            System.Windows.Forms.MessageBox.Show($"Borrowed Books:\n{borrowedBooks}") |> ignore
        else
            System.Windows.Forms.MessageBox.Show("No books borrowed currently.") |> ignore
