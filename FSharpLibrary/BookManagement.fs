﻿namespace LibraryManagement

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
