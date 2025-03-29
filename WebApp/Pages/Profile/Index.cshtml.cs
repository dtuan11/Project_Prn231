using API.DTO.Response;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Category> Categories { get; set; } = new List<Category>();
        public string? AccountId { get; set; }
        public ProfileResponse ProfileModel { get; set; } = new ProfileResponse();
        public List<ReadingResponse> Readings { get; set; } = new List<ReadingResponse>();
        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7186/"); 

            AccountId = HttpContext.Session.GetString("userId");

            Categories = await client.GetFromJsonAsync<List<Category>>("api/profile/categories") ?? new List<Category>();
            ProfileModel = await client.GetFromJsonAsync<ProfileResponse>($"api/profile/{id}") ?? new ProfileResponse();
            Readings = await client.GetFromJsonAsync<List<ReadingResponse>>($"api/profile/{id}/readings") ?? new List<ReadingResponse>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7186/");

            var form = await Request.ReadFormAsync();
            var accountId = int.Parse(form["userId"].ToString());

            var content = new MultipartFormDataContent
            {
                { new StringContent(accountId.ToString()), "AccountId" },
                { new StringContent(form["email"]), "Email" },
                { new StringContent(form["address"]), "Address" },
                { new StringContent(form["phone"]), "Phone" }
            };

            if (!string.IsNullOrEmpty(form["newPassword"]))
            {
                content.Add(new StringContent(form["currentPassword"]), "CurrentPassword");
                content.Add(new StringContent(form["newPassword"]), "NewPassword");
                content.Add(new StringContent(form["confirmPassword"]), "ConfirmPassword");
            }

            if (Request.Form.Files["File"] != null)
            {
                var file = Request.Form.Files["File"];
                content.Add(new StreamContent(file.OpenReadStream()), "AvatarFile", file.FileName);
            }

            var response = await client.PutAsync("api/profile", content);
            var result = await response.Content.ReadFromJsonAsync<ProfileResponse>();

            Message = result?.Message ?? "An error occurred";
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Profile/Index", new { id = accountId });
            }

            await OnGetAsync(accountId);
            return Page();
        }
    }
}
