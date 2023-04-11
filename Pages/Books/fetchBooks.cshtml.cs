using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
namespace WebApplication1.Pages.Books
{
    public class fetchBooksModel : PageModel
    {
        public List<Books> booksList;
        public void OnGet()
        {
            try
            {
                booksList = new List<Books>();
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ESRRQ99;Initial Catalog=lms_library ;Integrated Security=True;Encrypt = False;");
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();  
                cmd.CommandText = "SELECT BOOK_CODE,BOOK_TITLE,AUTHOR,PUBLICATION,PRICE FROM LMS_BOOK_DETAILS; ";
                SqlDataReader reader = cmd.ExecuteReader();
                
                while(reader.Read())
                {
                    Books book = new Books();

                    book.BookCode = (string)reader["BOOK_CODE"];
                    book.BookTitle = (string)reader["BOOK_TITLE"];
                    book.Author = (string)reader["AUTHOR"];
                    book.Publication = (string)reader["PUBLICATION"];
                    book.Price = (int)reader["price"];

                    booksList.Add(book);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class Books
    {
        public string BookCode { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

        public string Publication { get; set; }
        public DateTime PublishDate  { get; set; }
        public int BookEdition { get; set; }
        public float Price { get; set; }
        public string RackNum { get; set; }
        public DateTime DateArrival { get; set; }
        public string SupplierId { get; set; }

    }
}
