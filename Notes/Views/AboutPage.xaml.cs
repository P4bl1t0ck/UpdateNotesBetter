using Notes.Models;
using Notes.ViewModels;
using static Android.App.DownloadManager;

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
else
    AllNotes.Insert(0, new NoteViewModel(Models.Note.Load(noteId)));

}
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
{
    if (query.ContainsKey("deleted"))
    {
        string noteId = query["deleted"].ToString();
        NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

        // If note exists, delete it
        if (matchedNote != null)
            AllNotes.Remove(matchedNote);
    }
    else if (query.ContainsKey("saved"))
    {
        string noteId = query["saved"].ToString();
        NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

        // If note is found, update it
        if (matchedNote != null)
        {
            matchedNote.Reload();
            AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
        }
        // If note isn't found, it's new; add it.
        else
            AllNotes.Insert(0, new NoteViewModel(Models.Note.Load(noteId)));
    }
}
