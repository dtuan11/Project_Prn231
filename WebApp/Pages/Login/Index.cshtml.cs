using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public LoginModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string UserId { get; set; } = default!;
        [BindProperty]
        public User UserLogin { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        public void OnGet()
        {
            Categories = context.Categories.ToList();
        }
        public IActionResult OnPost()
        {
            UserLogin.Password = HashPassword(UserLogin.Password);
            var exist = context.Users.FirstOrDefault(x => x.UserName.Equals(UserLogin.UserName)
            && x.Password.Equals(UserLogin.Password));
            if (exist != null)
            {
                if(exist.Active == false)
                {
                    Message = "Tài khoản đang bị khóa";
                    return Page();
                }
                else
                {
                    HttpContext.Session.SetString("userId", exist.UserId.ToString());
                    return RedirectToPage("/Homepage/Index");
                }
            }
            else
            {
                Message = "Sai tên đăng nhập hoặc mật khẩu";
                return Page();
            }
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
