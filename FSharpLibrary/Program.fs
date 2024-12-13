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

        

        // Add controls to the form
        form.Controls.Add(titleLabel)
        form.Controls.Add(titleTextBox)
        form.Controls.Add(authorLabel)
        form.Controls.Add(authorTextBox)
        form.Controls.Add(genreLabel)
        form.Controls.Add(genreTextBox)
        form.Controls.Add(addBookButton)
        

        // Run the application
        Application.Run(form)

        // Return exit code (necessary for entry point)
        0
