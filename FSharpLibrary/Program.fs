namespace LibraryManagementApp

open System
open System.Windows.Forms
open LibraryManagement
open Library
open System.Drawing

module Program =

    [<EntryPoint>]
    let main _ =
        // Create the BookManagement instance
        let bookManagement = new BookManagement()

        // Load books from file when the application starts
        bookManagement.LoadBooksFromFile()  // No need to expect a return value

        // Create the form
        let form = new Form(Text = "Library Management System", Width = 500, Height = 700)
        form.BackColor <- Color.WhiteSmoke
        form.FormBorderStyle <- FormBorderStyle.FixedDialog
        form.MaximizeBox <- false
        form.StartPosition <- FormStartPosition.CenterScreen

        // Header label
        let headerLabel = new Label(
            Text = "Library Management System", 
            Font = new Font("Arial", 16.0f, FontStyle.Bold), 
            Top = 20, Left = 150, AutoSize = true, 
            ForeColor = Color.DarkBlue
        )
        form.Controls.Add(headerLabel)

        // Create labels and textboxes for adding books
        let titleLabel = new Label(Text = "Book Title:", Top = 70, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let titleTextBox = new TextBox(Top = 90, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        titleTextBox.BackColor <- Color.LightYellow

        let authorLabel = new Label(Text = "Author:", Top = 130, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let authorTextBox = new TextBox(Top = 150, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        authorTextBox.BackColor <- Color.LightYellow

        let genreLabel = new Label(Text = "Genre:", Top = 190, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let genreTextBox = new TextBox(Top = 210, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        genreTextBox.BackColor <- Color.LightYellow

        // Add Book button under Genre
        let addBookButton = new Button(Text = "Add Book", Top = 250, Left = 120, Width = 250)
        addBookButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        addBookButton.BackColor <- Color.LightGreen
        addBookButton.FlatStyle <- FlatStyle.Flat

        // Create buttons for other operations (Search, Borrow, Return, etc.)

        // Search Book section
        let searchTitleLabel = new Label(Text = "Search Book Title:", Top = 290, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let searchTitleTextBox = new TextBox(Top = 310, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        searchTitleTextBox.BackColor <- Color.LightYellow
        let searchBookButton = new Button(Text = "Search Book", Top = 340, Left = 120, Width = 250)
        searchBookButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        searchBookButton.BackColor <- Color.LightGoldenrodYellow
        searchBookButton.FlatStyle <- FlatStyle.Flat

        // Borrow Book section
        let borrowTitleLabel = new Label(Text = "Borrow Book Title:", Top = 380, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let borrowTitleTextBox = new TextBox(Top = 400, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        borrowTitleTextBox.BackColor <- Color.LightYellow
        let borrowBookButton = new Button(Text = "Borrow Book", Top = 430, Left = 120, Width = 250)
        borrowBookButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        borrowBookButton.BackColor <- Color.LightSkyBlue
        borrowBookButton.FlatStyle <- FlatStyle.Flat

        // Return Book section
        let returnTitleLabel = new Label(Text = "Return Book Title:", Top = 470, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let returnTitleTextBox = new TextBox(Top = 490, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        returnTitleTextBox.BackColor <- Color.LightYellow
        let returnBookButton = new Button(Text = "Return Book", Top = 520, Left = 120, Width = 250)
        returnBookButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        returnBookButton.BackColor <- Color.LightCoral
        returnBookButton.FlatStyle <- FlatStyle.Flat

        // Display Borrowed Books button
        let displayBorrowedBooksButton = new Button(Text = "Display Borrowed Books", Top = 550, Left = 120, Width = 250)
        displayBorrowedBooksButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        displayBorrowedBooksButton.BackColor <- Color.LightPink
        displayBorrowedBooksButton.FlatStyle <- FlatStyle.Flat

        // Display All Books button
        let displayBooksButton = new Button(Text = "Display All Books", Top = 580, Left = 120, Width = 250)
        displayBooksButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        displayBooksButton.BackColor <- Color.LightSkyBlue
        displayBooksButton.FlatStyle <- FlatStyle.Flat

        // Add Button Click Event Handlers
        addBookButton.Click.Add(fun _ -> 
            let title = titleTextBox.Text
            let author = authorTextBox.Text
            let genre = genreTextBox.Text
            if title <> "" && author <> "" && genre <> "" then
                bookManagement.AddBook(title, author, genre)
                MessageBox.Show("Book added successfully!") |> ignore
                bookManagement.SaveBooksToFile (bookManagement.GetBooks())  // Save after adding
            else
                MessageBox.Show("Please fill all fields.") |> ignore
        )

        searchBookButton.Click.Add(fun _ ->
            let searchTerm = searchTitleTextBox.Text
            if searchTerm <> "" then
                let result = bookManagement.SearchBookByTitle(searchTerm)
                let resultString = 
                    result |> List.map (fun book -> $"{book.Title} by {book.Author} ({book.Genre})") |> String.concat "\n"
                MessageBox.Show(if resultString = "" then "No books found." else resultString) |> ignore
            else
                MessageBox.Show("Please enter a book title to search.") |> ignore
        )

        borrowBookButton.Click.Add(fun _ -> 
            let title = borrowTitleTextBox.Text
            if title <> "" then
                bookManagement.BorrowBook(title)
            else
                MessageBox.Show("Please enter a book title to borrow.") |> ignore
        )

        returnBookButton.Click.Add(fun _ -> 
            let title = returnTitleTextBox.Text
            if title <> "" then
                bookManagement.ReturnBook(title)
            else
                MessageBox.Show("Please enter a book title to return.") |> ignore
        )

        displayBorrowedBooksButton.Click.Add(fun _ -> 
            bookManagement.ViewBorrowedBooks()
        )

        displayBooksButton.Click.Add(fun _ -> 
            let books = bookManagement.GetBooks()
            let bookList = 
                books |> List.map (fun book -> $"{book.Title} by {book.Author} ({book.Genre})") |> String.concat "\n"
            MessageBox.Show(bookList) |> ignore
        )

        // Add controls to the form
        form.Controls.Add(headerLabel)
        form.Controls.Add(titleLabel)
        form.Controls.Add(titleTextBox)
        form.Controls.Add(authorLabel)
        form.Controls.Add(authorTextBox)
        form.Controls.Add(genreLabel)
        form.Controls.Add(genreTextBox)
        form.Controls.Add(addBookButton)
        form.Controls.Add(searchTitleLabel)
        form.Controls.Add(searchTitleTextBox)
        form.Controls.Add(searchBookButton)
        form.Controls.Add(borrowTitleLabel)
        form.Controls.Add(borrowTitleTextBox)
        form.Controls.Add(borrowBookButton)
        form.Controls.Add(returnTitleLabel)
        form.Controls.Add(returnTitleTextBox)
        form.Controls.Add(returnBookButton)
        form.Controls.Add(displayBorrowedBooksButton)
        form.Controls.Add(displayBooksButton)

        // Run the application
        Application.Run(form)

        // Return exit code (necessary for entry point)
        0
