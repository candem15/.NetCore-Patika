using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;

namespace WebApi.Operations.BookOperations.Commands
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }
        readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(
            BookStoreDbContext dbContext,
            int itemId,
            UpdateBookModel model
        )
        {
            _dbContext = dbContext;
            Id = itemId;
            Model = model;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Id == Id);
            if (book == null)
                throw new FileNotFoundException("Kitap bulunamadı!");

            if (_dbContext.Books.Any(a => a.Title.ToLower() == Model.Title.ToLower() && a.Id != Id))
                throw new Exception(
                    "Bu isimde bir kitap hali hazırda mevcuttur. Başka bir isim deneyiniz!"
                );

            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }

}