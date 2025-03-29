using API.Controllers;
using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace WebApp.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }

        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; } = default!;
        [BindProperty]
        public User UserModel { get; set; } = default!;
        [BindProperty]
        public IFormFile File { get; set; }
        public List<ReadingResponse> Readings { get; set; } = new List<ReadingResponse>();
        public User user { get; set; }

        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync(int id)
        {

            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            UserId = HttpContext.Session.GetString("userId");
            var readingResponse = await _httpClient.GetAsync("api/Profile/" + UserId + "/readings");
            if (readingResponse.IsSuccessStatusCode)
            {
                Readings = await readingResponse.Content.ReadFromJsonAsync<List<ReadingResponse>>();
            }
            var userResponse = await _httpClient.GetAsync("userDetail/" + int.Parse(UserId));
            if (userResponse.IsSuccessStatusCode)
            {
                user = await userResponse.Content.ReadFromJsonAsync<User>();
            }


            var exist = context.Users.Include(u => u.Role).FirstOrDefault(x => x.UserId == id);
            if (exist != null)
            if (user != null)
            {
                UserModel = user;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string newPassword = Request.Form["newPassword"];
            string currentPassword = Request.Form["currentPassword"];
            if (currentPassword != null)
            {
                currentPassword = HashPassword(currentPassword);
            }
            string confirmPassword = Request.Form["confirmPassword"];
            string userId = Request.Form["userId"];
            var userResponse = await _httpClient.GetAsync("userDetail/" + int.Parse(userId));
            if (userResponse.IsSuccessStatusCode)
            {
                user = await userResponse.Content.ReadFromJsonAsync<User>();
            }
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
                            var request = new UserUpdateRequest
                            {
                                Password = user.Password,
                                Email = user.Email,
                                Address = user.Address,
                                Phone = user.Phone,
                                Id = user.UserId,
                                //Avatar = string.IsNullOrEmpty(user.Avatar)?"":user.Avatar,
                            };
                            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                            var res = await _httpClient.PutAsync("api/Users", jsonContent);
                            if (res.IsSuccessStatusCode) 
                            {
                                return RedirectToPage("/Profile/Index", new { id = user.UserId });
                            }
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
                    var request = new UserUpdateRequest
                    {
                        Password = user.Password,
                        Email = user.Email,
                        Address = user.Address,
                        Phone = user.Phone,
                        Id = user.UserId,
                        //Avatar = string.IsNullOrEmpty(user.Avatar) ? "" : user.Avatar,

                    };
                    var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                    var res = await _httpClient.PutAsync("api/Users", jsonContent);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToPage("/Profile/Index", new { id = user.UserId });
                    }
                    UserModel = user;
                    

                }
            }

            return RedirectToPage("/Profile/Index", new { id = user.UserId });
            
            //var request = new UserUpdateRequest
            //{
            //    CurrentPassword = currentPassword,
            //    NewPassword = newPassword,
            //    ConfirmPassword = confirmPassword,
            //    Email = user.Email,
            //    Address = user.Address,
            //    Phone = user.Phone,
            //    Id = user.UserId

            //};
            //var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            //var res = await _httpClient.PutAsync("api/Users", jsonContent);

            //if (res.IsSuccessStatusCode)
            //{
            //    Message = "Thanh cong";
            //    return RedirectToPage("/Profile/Index", new { id = user.UserId });
            //}
            //else
            //{
            //    Message = "That bai";
            //    return RedirectToPage("/Profile/Index", new { id = user.UserId });
            //}
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
