using BitzArt.Blazor.MVVM;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp.Client.Pages;

public partial class BooksPage : PageBase<BooksPageViewModel>
{
    protected BooksProvider Provider => ViewModel.BooksProvider;
}

public static class BookExtensions
{
    public static int GetStarCount(this Book book) => Convert.ToInt32(book.Rating!.Rating!.Value) / 1;
}