namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Text;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        public static void Main()
        {
            using var context = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            Console.WriteLine(GetMostRecentBooks(context));
        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if(!Enum.TryParse(command, true, out AgeRestriction ageRestriction))
            {
                return string.Empty;
            }
            var bookTitle = context.Books
                .Where(x=> x.AgeRestriction == ageRestriction)
                .Select(x => x.Title)
                .OrderBy(e=> e)
                .ToArray();

            return string.Join(Environment.NewLine, bookTitle);
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenEditionBooks = context.Books
                .Where(n=> n.EditionType ==EditionType.Gold && n.Copies < 5000)
                .Select(b => b.Title)
                .OrderBy(e=> e)
                .ToArray();
            return string.Join(Environment.NewLine, goldenEditionBooks);
        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            var mostExpensiveBooks = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price,
                })
                .OrderBy(e => e.Price)
                .ToArray();
            return string.Join(Environment.NewLine, mostExpensiveBooks.Select(a=> $"{a.Title} - {a.Price:f2}"));
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year != year)
                .Select(b => new { b.Title, b.BookId })
                .OrderBy(e => e.BookId)
                .ToArray();
            return string.Join(Environment.NewLine, books.Select(a=>a.Title));
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[]categories = input.ToLower().Split(' ',StringSplitOptions.RemoveEmptyEntries);
            var booksByCategory = context.BooksCategories
                .Where(b=>categories.Contains(b.Category.Name))
                .Select(b=> b.Book.Title)
                .OrderBy(t=>t)
                .ToArray();
            return string.Join(Environment.NewLine, booksByCategory);
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dt = DateTime.Parse(date);
            var booksReleasedBefore = context.Books
                .Where(b=>b.ReleaseDate < dt)
                .OrderBy(b => b.ReleaseDate)
                .Select(b=> $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                .ToArray();

            return string.Join(Environment.NewLine, booksReleasedBefore);
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a=> a.FirstName.EndsWith(input))
                .Select(a=> $"{a.FirstName} {a.LastName}")
                .ToArray()
                .OrderBy(a=> a) .ToArray();
            return string.Join (Environment.NewLine, authors);
        }
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string lowerdInput = input.ToLower();
            var bookTitles = context.Books
                .Where(b=> b.Title.ToLower().Contains(lowerdInput))
                .Select(b=>b.Title)
                .OrderBy(b=>b) .ToArray();
            return string.Join(Environment.NewLine,bookTitles);
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            string inputLowered = input.ToLower();

            var booksAuthor = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(inputLowered))
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();
            return string.Join(Environment.NewLine , booksAuthor);
        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books.Count(b=>b.Title.Length > lengthCheck);
        }
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorsCopies = context.Authors
                .Select(a=> new
                {
                    a.FirstName,
                    a.LastName,
                    Copies = a.Books.Sum(b=>b.Copies)
                })
                .OrderByDescending(a=>a.Copies)
                .ToArray();
            return string.Join(Environment.NewLine, authorsCopies.Select(ac=> $"{ac.FirstName} {ac.LastName} {ac.Copies}"));
        }
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoriesByProfit = context.Categories
                .Select(c=> new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Sum(cb=> cb.Book.Price*cb.Book.Copies)
                })
                .OrderByDescending(c=> c.Profit)
                .ThenBy(c=> c.Name)
                .ToArray();
            return string.Join(Environment.NewLine, categoriesByProfit.Select(c=>$"{c.Name} ${c.Profit:f2}"));
        }
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categoriesWithLatestThreebooks = context.Categories
                .Select(c => new
                {
                    c.Name,
                    MostRecentBooks = c.CategoryBooks
                    .OrderByDescending(b => b.Book.ReleaseDate)
                    .Take(3)
                    .Select(b => $"{b.Book.Title} ({b.Book.ReleaseDate.Value.Year}")
                }).ToArray()
                .OrderBy(x => x.Name)
                .ToArray();
            var sb = new StringBuilder();
            foreach (var c in categoriesWithLatestThreebooks)
            {
                sb.AppendLine($"-- {c.Name}");
                foreach (var item in c.MostRecentBooks)
                {
                    sb.AppendLine(item);
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}


