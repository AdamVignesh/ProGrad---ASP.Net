using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication1.Pages.Books
{
    public class AddBookModel : PageModel
    {
        public string message = "";
        public void OnPost()
        {
            message = "";
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ESRRQ99;Initial Catalog=lms_library ;Integrated Security=True;Encrypt = False;");
                conn.Open();
                Books b = new Books();

                b.BookCode = Request.Form["BookCode"];
                b.BookTitle = Request.Form["BookTitle"];
                b.Author = Request.Form["Author"];
                b.Category = Request.Form["Category"];
                b.Publication = Request.Form["Publication"];
                b.PublishDate = Convert.ToDateTime(Request.Form["PublicDate"]);
                b.BookEdition = Convert.ToInt32(Request.Form["BookEdition"]);
                b.Price = Convert.ToInt32(Request.Form["Price"]);

                b.RackNum = "A1";
                b.DateArrival = Convert.ToDateTime("2021-04-11");
                b.SupplierId = "S03";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO LMS_BOOK_DETAILS(BOOK_CODE,BOOK_TITLE,"+
                    $"AUTHOR,CATEGORY,PUBLICATION,PUBLISH_DATE,BOOK_EDITION,PRICE,RACK_NUM,DATE_ARRIVAL,"+
                    $"SUPPLIER_ID) VALUES ('{b.BookCode}','{b.BookTitle}','{b.Author}'," +
                    $"'{b.Category}','{b.Publication}','{b.PublishDate}',{b.BookEdition},{b.Price},'{b.RackNum}'," +
                    $"'{b.DateArrival}','{b.SupplierId}');";

                cmd.ExecuteNonQuery();

                message = "Book registered successfully";
            }
            catch(Exception ex) 
            {
                message = ex.Message;
            }
        }
    }
}
