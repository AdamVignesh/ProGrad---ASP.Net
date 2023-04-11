using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class UpdateBookDetailsModel : PageModel
    {
        public Books b = new Books();
        public string message = "";
        public string BookCode = "";
        public void OnGet()
        {
            try
            {
                BookCode = Request.Query["BookCode"];
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ESRRQ99;Initial Catalog=lms_library ;Integrated Security=True;Encrypt = False;");
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT BOOK_TITLE,AUTHOR,CATEGORY,PUBLICATION," +
                   $"PUBLISH_DATE,BOOK_EDITION,PRICE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{BookCode}';";

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    b.BookTitle = (string)reader["BOOK_TITLE"];
                    b.Author = (string)reader["AUTHOR"];
                    b.Category = (string)reader["CATEGORY"];
                    b.Publication = (string)reader["PUBLICATION"];
                    b.PublishDate = (DateTime)reader["PUBLISH_DATE"];
                    b.BookEdition = (int)reader["BOOK_EDITION"];
                    b.Price = (int)reader["PRICE"];
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void OnPost()
        {
            try
            {
                message = "";
                BookCode = Request.Query["BookCode"];
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ESRRQ99;Initial Catalog=lms_library ;Integrated Security=True;Encrypt = False;");
                conn.Open();

                b.BookTitle = Request.Form["BookTitle"];
                b.Author = Request.Form["Author"];
                b.Category = Request.Form["Category"];
                b.Publication = Request.Form["Publication"];
                b.PublishDate = Convert.ToDateTime(Request.Form["PublishDate"]);
                b.BookEdition = Convert.ToInt32(Request.Form["BookEdition"]);
                b.Price = Convert.ToInt32(Request.Form["Price"]);

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = $"UPDATE LMS_BOOK_DETAILS SET BOOK_TITLE ='{b.BookTitle}'," +
                    $"AUTHOR = '{b.Author}',CATEGORY = '{b.Category}', PUBLICATION = '{b.Publication}'," +
                    $"PUBLISH_DATE = '{b.PublishDate}', BOOK_EDITION = {b.BookEdition}, PRICE = {b.Price} WHERE " +
                    $"BOOK_CODE = '{BookCode}'";

                cmd.ExecuteNonQuery();

                message = "Book updated successfully";

            }
            catch(Exception ex)
            {
                message = ex.Message;

            }

        }
        
    }
}
