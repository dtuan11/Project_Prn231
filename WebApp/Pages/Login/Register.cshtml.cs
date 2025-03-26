using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public RegisterModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }

        [BindProperty]
        public User UserModel { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var existUsername = context.Users.FirstOrDefault(x => x.UserName.Equals(UserModel.UserName));
            if (existUsername != null)
            {
                Message = "Tên đăng nhập đã tồn tại";
                return Page();
            }
            string confirmPassword = Request.Form["confirmPassword"];
            if (!confirmPassword.Equals(UserModel.Password))
            {
                Message = "Mật khẩu nhập lại không trùng";
                return Page();
            }
            UserModel.Password = HashPassword(UserModel.Password);
            UserModel.Active = true;
            context.Users.Add(UserModel);
            context.SaveChanges();
            HttpContext.Session.SetString("userId", UserModel.UserId.ToString());
            return RedirectToPage("/Homepage/Index");
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
