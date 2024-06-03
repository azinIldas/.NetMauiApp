using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NotePage : ContentPage
    {
        public NotePage()
        {
            InitializeComponent();
        }

        public string ItemId
        {
            set { LoadNote(value); }
        }

        private void LoadNote(string filename)
        {
            Models.Note note = new Models.Note();
            note.Filename = filename;

            if (File.Exists(filename))
                note.Text = File.ReadAllText(filename);

            BindingContext = note;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Models.Note note)
                File.WriteAllText(note.Filename, TextEditor.Text);

            await Shell.Current.GoToAsync("..");
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Models.Note note)
            {
                // Delete the file.
                if (File.Exists(note.Filename))
                    File.Delete(note.Filename);
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
