using API.DAO.IDAO;
using API.DTO.Request;
using API.DTO.Response;
using API.Models;
using API.Repositories.IRepositories;
using System.Security.Cryptography;
using System.Text;

namespace API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IProfileDao _profileDao;

        public ProfileRepository(IProfileDao profileDao)
        {
            _profileDao = profileDao;
        }

        public async Task<ProfileResponse> GetProfileAsync(int id)
        {
            var account = await _profileDao.GetAccountByIdAsync(id);
            if (account == null) return null;

            return new ProfileResponse
            {
                AccountId = account.UserId,
                UserName = account.UserName,
                Email = account.Email,
                Address = account.Address,
                Phone = account.Phone,
                Avatar = account.Avatar,
                RoleId = account.RoleId
            };
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _profileDao.GetCategoriesAsync();
        }

        public async Task<List<ReadingResponse>> GetAccountReadingsAsync(int accountId)
        {
            var readings = await _profileDao.GetReadingsByAccountIdAsync(accountId);
            return readings.Select(r => new ReadingResponse
            {
                BookTitle = r.Book.Title,
                BookImage = r.Book.Img,
                BookId = r.Book.BookId,
                ChapterId = r.Chapter.ChapterId,
                ChapterName = r.Chapter.ChapterName,
                ReadingDate = r.ReadingDate
            }).ToList();
        }

        public async Task<ProfileResponse> UpdateProfileAsync(ProfileRequest request)
        {
            var account = await _profileDao.GetAccountByIdAsync(request.AccountId);
            if (account == null) return new ProfileResponse { Message = "Account not found" };

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                string hashedCurrent = HashPassword(request.CurrentPassword);
                if (hashedCurrent != account.Password)
                    return new ProfileResponse { Message = "Sai mật khẩu hiện tại" };
                if (request.NewPassword != request.ConfirmPassword)
                    return new ProfileResponse { Message = "Nhập lại không trùng khớp" };
                if (request.NewPassword == request.CurrentPassword)
                    return new ProfileResponse { Message = "Mật khẩu mới trùng với mật khẩu cũ" };

                account.Password = HashPassword(request.NewPassword);
            }
            else
            {
                if (request.AvatarFile != null && request.AvatarFile.Length > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(folderPath);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.AvatarFile.FileName);
                    var filePath = Path.Combine(folderPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.AvatarFile.CopyToAsync(stream);
                    }
                    account.Avatar = "/images/" + fileName;
                }
                account.Email = request.Email;
                account.Address = request.Address;
                account.Phone = request.Phone;
            }

            await _profileDao.UpdateAccountAsync(account);
            return new ProfileResponse
            {
                AccountId = account.UserId,
                UserName = account.UserName,
                Email = account.Email,
                Address = account.Address,
                Phone = account.Phone,
                Avatar = account.Avatar,
                RoleId = account.RoleId,
                Message = "Cập nhật thành công"
            };
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
