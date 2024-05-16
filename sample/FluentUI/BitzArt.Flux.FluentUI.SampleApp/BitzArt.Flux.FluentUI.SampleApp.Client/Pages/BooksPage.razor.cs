using BitzArt.Blazor.MVVM;

namespace BitzArt.Flux.FluentUI.SampleApp.Client.Pages;

public partial class BooksPage : PageBase<BooksPageViewModel>
{
    protected BooksProvider Provider => ViewModel.BooksProvider;
}
