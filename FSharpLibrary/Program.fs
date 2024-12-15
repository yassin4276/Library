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
        bookManagement.LoadBooksFromFile()

        // Create the form
        let form = new Form(Text = "Library Management System", Width = 500, Height = 600)
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
        let titleTextBox = new TextBox(Top = 70, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        titleTextBox.BackColor <- Color.LightYellow

        let authorLabel = new Label(Text = "Author:", Top = 110, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let authorTextBox = new TextBox(Top = 110, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        authorTextBox.BackColor <- Color.LightYellow

        let genreLabel = new Label(Text = "Genre:", Top = 150, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let genreTextBox = new TextBox(Top = 150, Left = 120, Width = 250, Font = new Font("Arial", 10.0f))
        genreTextBox.BackColor <- Color.LightYellow

        let addBookButton = new Button(Text = "Add Book", Top = 190, Left = 120, Width = 250)
        addBookButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        addBookButton.BackColor <- Color.LightGreen
        addBookButton.FlatStyle <- FlatStyle.Flat
        addBookButton.Click.Add(fun _ -> 
            let title = titleTextBox.Text
            let author = authorTextBox.Text
            let genre = genreTextBox.Text
            if title <> "" && author <> "" && genre <> "" then
                bookManagement.AddBook(title, author, genre)
                MessageBox.Show("Book added successfully!") |> ignore
                bookManagement.SaveBooksToFile()  
            else
                MessageBox.Show("Please fill all fields.") |> ignore
        )

        // Create and set up controls for borrowing books
        let borrowTitleLabel = new Label(Text = "Borrow Book Title:", Top = 230, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let borrowTitleTextBox = new TextBox(Top = 230, Left = 120 + 20, Width = 250, Font = new Font("Arial", 10.0f))  // Move right by 20 pixels
        borrowTitleTextBox.BackColor <- Color.LightYellow

        let borrowButton = new Button(Text = "Borrow Book", Top = 270, Left = 120, Width = 250)
        borrowButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        borrowButton.BackColor <- Color.LightYellow
        borrowButton.FlatStyle <- FlatStyle.Flat
        borrowButton.Click.Add(fun _ -> 
            let title = borrowTitleTextBox.Text
            bookManagement.BorrowBook(title)
            bookManagement.SaveBooksToFile()  
        )

        // Create and set up controls for returning books
        let returnTitleLabel = new Label(Text = "Return Book Title:", Top = 310, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let returnTitleTextBox = new TextBox(Top = 310, Left = 120 + 20, Width = 250, Font = new Font("Arial", 10.0f))  // Move right by 20 pixels
        returnTitleTextBox.BackColor <- Color.LightYellow

        let returnButton = new Button(Text = "Return Book", Top = 350, Left = 120, Width = 250)
        returnButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        returnButton.BackColor <- Color.LightCoral
        returnButton.FlatStyle <- FlatStyle.Flat
        returnButton.Click.Add(fun _ -> 
            let title = returnTitleTextBox.Text
            bookManagement.ReturnBook(title)
            bookManagement.SaveBooksToFile()  
        )

        // Create a button to display all books
        let displayBooksButton = new Button(Text = "Display All Books", Top = 390, Left = 120, Width = 250)
        displayBooksButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        displayBooksButton.BackColor <- Color.LightSkyBlue
        displayBooksButton.FlatStyle <- FlatStyle.Flat
        displayBooksButton.Click.Add(fun _ -> 
            bookManagement.DisplayBooks()
        )

        // Create a button to view borrowed books
        let viewBorrowedBooksButton = new Button(Text = "View Borrowed Books", Top = 430, Left = 120, Width = 250)
        viewBorrowedBooksButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        viewBorrowedBooksButton.BackColor <- Color.LightSteelBlue
        viewBorrowedBooksButton.FlatStyle <- FlatStyle.Flat
        viewBorrowedBooksButton.Click.Add(fun _ -> 
            bookManagement.ViewBorrowedBooks()
        )

        // Add search functionality
        // Search Label and Textbox
        let searchLabel = new Label(Text = "Search Book by Title:", Top = 470, Left = 20, AutoSize = true, Font = new Font("Arial", 10.0f))
        let searchTextBox = new TextBox(Top = 470, Left = 120 + 35, Width = 250, Font = new Font("Arial", 10.0f))  // Move right by 20 pixels
        searchTextBox.BackColor <- Color.LightYellow

        // Search Button
        let searchButton = new Button(Text = "Search", Top = 510, Left = 120, Width = 250)
        searchButton.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
        searchButton.BackColor <- Color.LightCyan
        searchButton.FlatStyle <- FlatStyle.Flat
        searchButton.Click.Add(fun _ -> 
            let searchTitle = searchTextBox.Text
            match bookManagement.SearchBookByTitle(searchTitle) with
            | [book] -> 
                MessageBox.Show($"Found: {book.Title} by {book.Author} ({book.Genre})") |> ignore
            | _ -> 
                MessageBox.Show("Book not found.") |> ignore
        )

        // Add controls to the form
        form.Controls.Add(titleLabel)
        form.Controls.Add(titleTextBox)
        form.Controls.Add(authorLabel)
        form.Controls.Add(authorTextBox)
        form.Controls.Add(genreLabel)
        form.Controls.Add(genreTextBox)
        form.Controls.Add(addBookButton)

        form.Controls.Add(borrowTitleLabel)
        form.Controls.Add(borrowTitleTextBox)
        form.Controls.Add(borrowButton)

        form.Controls.Add(returnTitleLabel)
        form.Controls.Add(returnTitleTextBox)
        form.Controls.Add(returnButton)

        form.Controls.Add(displayBooksButton)
        form.Controls.Add(viewBorrowedBooksButton)

        form.Controls.Add(searchLabel)
        form.Controls.Add(searchTextBox)
        form.Controls.Add(searchButton)

        // Run the application
        Application.Run(form)

        // Return exit code (necessary for entry point)
        0

