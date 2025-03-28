using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public IndexModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        [BindProperty]
        public User UserModel { get; set; } = default!;
        [BindProperty]
        public IFormFile File { get; set; }
        public List<Reading>Readings { get; set; } = new List<Reading>();
        public User user { get; set; }

        public string Message { get; set; } = string.Empty;
        public void OnGet(int id)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            Readings = context.Readings.Include(x=>x.Book).Include(x=>x.Chapter).Where(x=>x.UserId == id).ToList();
            user = context.Users.FirstOrDefault(x => x.UserId == int.Parse(UserId));

            var exist = context.Users.Include(u => u.Role).FirstOrDefault(x => x.UserId == id);
            if (exist != null)
            {
                UserModel = exist;
            }
        }
        public IActionResult OnPost()
        {

            string newPassword = Request.Form["newPassword"];
            string currentPassword = Request.Form["currentPassword"];
            if(currentPassword != null)
            {
                currentPassword = HashPassword(currentPassword);
            }
            string confirmPassword = Request.Form["confirmPassword"];
            string userId = Request.Form["userId"];
            var user = context.Users.Find(int.Parse(userId));
            if (user != null)
            {
                if (!string.IsNullOrEmpty(newPassword))
                {
                    if (currentPassword.Equals(user.Password))
                    {
                        if (newPassword.Equals(currentPassword))
                        {
                            Message = "Mật khẩu mới đang trùng với mật khẩu cũ";
                        }
                        else if (!newPassword.Equals(confirmPassword))
                        {
                            Message = "Nhập lại không trùng khớp";
                        }
                        else
                        {
                            user.Password = HashPassword(newPassword);
                            UserModel = user;
                            context.Users.Update(user);
                            context.SaveChanges();
                            Message = "Đổi mật khẩu thành công";
                        }
                    }
                    else
                    {
                        Message = "Sai mật khẩu hiện tại";
                    }
                }
                else
                {
                    if (File != null)
                    {
                        
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                     
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                       
                        var filePath = Path.Combine(folderPath, File.FileName);

                       
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                             File.CopyTo(stream);
                        }
                    }
                    string email = Request.Form["email"];
                    string address = Request.Form["address"];
                    string phone = Request.Form["phone"];
                    if (File != null)
                    {
                        user.Avatar = "/images/" + File.FileName;
                    }
                    user.Email = email;
                    user.Address = address;
                    user.Phone = phone;
                    UserModel = user;
                    context.Users.Update(user);
                    context.SaveChanges();

                }
            }
            
            return RedirectToPage("/Profile/Index",new {id=user.UserId});
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
