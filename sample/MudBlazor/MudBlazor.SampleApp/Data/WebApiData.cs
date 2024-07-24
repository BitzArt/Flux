namespace MudBlazor.SampleApp;

internal static class WebApiData
{
    public static readonly Author[] Authors =
    [
        new() { Id = 1, Name = "George R. R. Martin" },
        new() { Id = 2, Name = "J. R. R. Tolkien" },
        new() { Id = 3, Name = "Stephen King" },
        new() { Id = 4, Name = "J. K. Rowling" },
        new() { Id = 5, Name = "Dante Alighieri" },
        new() { Id = 6, Name = "Homer" },
        new() { Id = 7, Name = "Virgil" }
    ];

    public static readonly Book[] Books =
    [
        new() { Id = 101, Title = "A Game of Thrones", AuthorId = 1, PublishYear = 1996 },
        new() { Id = 102, Title = "A Clash of Kings", AuthorId = 1, PublishYear = 1998 },
        new() { Id = 103, Title = "A Storm of Swords", AuthorId = 1, PublishYear = 2000 },
        new() { Id = 104, Title = "A Feast for Crows", AuthorId = 1, PublishYear = 2005 },
        new() { Id = 105, Title = "A Dance with Dragons", AuthorId = 1, PublishYear = 2011 },
        new() { Id = 106, Title = "The Winds of Winter", AuthorId = 1, PublishYear = 2023 },

        new() { Id = 201, Title = "The Hobbit", AuthorId = 2, PublishYear = 1937 },
        new() { Id = 202, Title = "The Lord of the Rings", AuthorId = 2, PublishYear = 1954 },
        new() { Id = 203, Title = "The Silmarillion", AuthorId = 2, PublishYear = 1977 },
        new() { Id = 204, Title = "Unfinished Tales", AuthorId = 2, PublishYear = 1980 },

        new() { Id = 301, Title = "Carrie", AuthorId = 3, PublishYear = 1974 },
        new() { Id = 302, Title = "The Shining", AuthorId = 3, PublishYear = 1977 },
        new() { Id = 303, Title = "It", AuthorId = 3, PublishYear = 1986 },
        new() { Id = 304, Title = "The Stand", AuthorId = 3, PublishYear = 1978 },
        new() { Id = 305, Title = "Misery", AuthorId = 3, PublishYear = 1987 },
        new() { Id = 306, Title = "The Dark Tower", AuthorId = 3, PublishYear = 1982 },

        new() { Id = 401, Title = "Harry Potter and the Philosopher's Stone", AuthorId = 4, PublishYear = 1997 },
        new() { Id = 402, Title = "Harry Potter and the Chamber of Secrets", AuthorId = 4, PublishYear = 1998 },
        new() { Id = 403, Title = "Harry Potter and the Prisoner of Azkaban", AuthorId = 4, PublishYear = 1999 },
        new() { Id = 404, Title = "Harry Potter and the Goblet of Fire", AuthorId = 4, PublishYear = 2000 },
        new() { Id = 405, Title = "Harry Potter and the Order of the Phoenix", AuthorId = 4, PublishYear = 2003 },
        new() { Id = 406, Title = "Harry Potter and the Half-Blood Prince", AuthorId = 4, PublishYear = 2005 },
        new() { Id = 407, Title = "Harry Potter and the Deathly Hallows", AuthorId = 4, PublishYear = 2007 },

        new() { Id = 501, Title = "Inferno", AuthorId = 5, PublishYear = 1320 },
        new() { Id = 502, Title = "Purgatorio", AuthorId = 5, PublishYear = 1321 },
        new() { Id = 503, Title = "Paradiso", AuthorId = 5, PublishYear = 1321 },
        new() { Id = 504, Title = "La Vita Nuova", AuthorId = 5, PublishYear = 1295 },
        new() { Id = 505, Title = "De Monarchia", AuthorId = 5, PublishYear = 1313 },
        new() { Id = 506, Title = "Convivio", AuthorId = 5, PublishYear = 1307 },
        new() { Id = 507, Title = "Rime", AuthorId = 5, PublishYear = 1292 },
        new() { Id = 508, Title = "Vita Nuova", AuthorId = 5, PublishYear = 1295 },
        new() { Id = 509, Title = "De Vulgari Eloquentia", AuthorId = 5, PublishYear = 1302 },
        new() { Id = 510, Title = "Eclogues", AuthorId = 5, PublishYear = 1308 },
        new() { Id = 511, Title = "Quaestio de Aqua et Terra", AuthorId = 5, PublishYear = 1320 },
        new() { Id = 512, Title = "Epistle to Cangrande", AuthorId = 5, PublishYear = 1314 },
        new() { Id = 513, Title = "Epistle to the Florentines", AuthorId = 5, PublishYear = 1304 },
        new() { Id = 514, Title = "Corpus Christi", AuthorId = 5, PublishYear = 1304 },
        new() { Id = 515, Title = "De Monarchia", AuthorId = 5, PublishYear = 1313 },

        new() { Id = 601, Title = "The Iliad", AuthorId = 6, PublishYear = -750 },
        new() { Id = 602, Title = "The Odyssey", AuthorId = 6, PublishYear = -720 },
        new() { Id = 603, Title = "The Homeric Hymns", AuthorId = 6, PublishYear = -700 },
        new() { Id = 604, Title = "The Epic Cycle", AuthorId = 6, PublishYear = -750 },
        new() { Id = 605, Title = "The Theban Cycle", AuthorId = 6, PublishYear = -750 },
        new() { Id = 606, Title = "The Trojan Cycle", AuthorId = 6, PublishYear = -750 },
        new() { Id = 607, Title = "The Cypria", AuthorId = 6, PublishYear = -750 },
        new() { Id = 608, Title = "The Aethiopis", AuthorId = 6, PublishYear = -750 },
        new() { Id = 609, Title = "The Little Iliad", AuthorId = 6, PublishYear = -750 },
        new() { Id = 610, Title = "The Sack of Troy", AuthorId = 6, PublishYear = -750 },
        new() { Id = 611, Title = "The Returns", AuthorId = 6, PublishYear = -750 },
        new() { Id = 612, Title = "The Telegony", AuthorId = 6, PublishYear = -750 },
        new() { Id = 613, Title = "The Thebaid", AuthorId = 6, PublishYear = -750 },
        new() { Id = 614, Title = "The Epigoni", AuthorId = 6, PublishYear = -750 },
        new() { Id = 615, Title = "The Cyclic Epics", AuthorId = 6, PublishYear = -750 },

        new() { Id = 701, Title = "Aeneid", AuthorId = 7, PublishYear = 19 },
        new() { Id = 702, Title = "Eclogues", AuthorId = 7, PublishYear = 37 },
        new() { Id = 703, Title = "Georgics", AuthorId = 7, PublishYear = 29 },
        new() { Id = 704, Title = "Culex", AuthorId = 7, PublishYear = 29 },
        new() { Id = 705, Title = "Ciris", AuthorId = 7, PublishYear = 29 },
        new() { Id = 706, Title = "Aetna", AuthorId = 7, PublishYear = 29 },
        new() { Id = 707, Title = "The Disaster of the City of Rome", AuthorId = 7, PublishYear = 29 },
        new() { Id = 708, Title = "Thebaid", AuthorId = 7, PublishYear = 29 },
        new() { Id = 709, Title = "Punica", AuthorId = 7, PublishYear = 17 },
        new() { Id = 710, Title = "Silvae", AuthorId = 7, PublishYear = 96 }
    ];

    public static readonly IDictionary<int, IEnumerable<Book>> BooksMap = Books
        .GroupBy(x => x.AuthorId!.Value)
        .ToDictionary(x => x.Key, x => x.AsEnumerable());
}
