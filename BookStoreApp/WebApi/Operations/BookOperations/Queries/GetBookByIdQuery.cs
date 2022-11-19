using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Operations.BookOperations.Queries
{
    public class GetBookByIdQuery
    {
        public int Id { get; set; }
        readonly BookStoreDbContext _dbContext;

        public GetBookByIdQuery(BookStoreDbContext dbContext, int itemId)
        {
            _dbContext = dbContext;
            Id = itemId;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);

            if (book is null)
                throw new FileNotFoundException("Kitap bulunamadı!");


            return new BookDetailViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Genre = ((BookGenresEnum)book.GenreId).ToString(),
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.ToString()
            };
        }
    }

    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}
