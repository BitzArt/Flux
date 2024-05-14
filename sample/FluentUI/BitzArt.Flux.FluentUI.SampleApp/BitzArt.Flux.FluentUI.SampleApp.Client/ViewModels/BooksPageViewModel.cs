using BitzArt.Blazor.MVVM;

namespace BitzArt.Flux.FluentUI.SampleApp.Client.ViewModels;

public class BooksPageViewModel : ViewModel<BooksPageState>
{
}

public class BooksPageState
{
    public int? AuthorId { get; set; }
}
