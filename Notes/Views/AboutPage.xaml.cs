using Notes.Models;
using Notes.ViewModels;

namespace Notes.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }
    string noteId = query["saved"].ToString();
    NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

// If note is found, update it
if (matchedNote != null)
{
    matchedNote.Reload();
    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
}
}