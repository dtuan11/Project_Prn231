using System;
using System.Collections.Generic;


namespace BusinessObject.Models
{
    public partial class PRN221_Project_1Context : DbContext
    {
        public PRN221_Project_1Context()
        {
        }

        public PRN221_Project_1Context(DbContextOptions<PRN221_Project_1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryInBook> CategoryInBooks { get; set; } = null!;
        public virtual DbSet<Chapter> Chapters { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Rate> Rates { get; set; } = null!;
        public virtual DbSet<Reading> Readings { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportType> ReportTypes { get; set; } = null!;
        public virtual DbSet<Response> Responses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SavedBook> SavedBooks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer(config.GetConnectionString("value")); }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).HasColumnName("bookID");

                entity.Property(e => e.Approve)
                    .HasMaxLength(50)
                    .HasColumnName(" approve");

                entity.Property(e => e.AuthorName).HasColumnName("authorName");

                entity.Property(e => e.Detail).HasColumnName("detail");

                entity.Property(e => e.Img).HasColumnName("img");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("date")
                    .HasColumnName("publishDate");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Views).HasColumnName("views");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Book_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CateId);

                entity.ToTable("Category");

                entity.Property(e => e.CateId)
                    .ValueGeneratedNever()
                    .HasColumnName("CateID");

                entity.Property(e => e.Describe)
                    .HasMaxLength(500)
                    .HasColumnName("describe");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CategoryInBook>(entity =>
            {
                entity.ToTable("Category_in_book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CategoryInBooks)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_in_book_Book");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.CategoryInBooks)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_in_book_Category");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.ToTable("Chapter");

                entity.Property(e => e.ChapterId).HasColumnName("ChapterID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.ChapterName).HasMaxLength(50);

                entity.Property(e => e.Contents1).HasColumnName("Contents_1");

                entity.Property(e => e.Contents2).HasColumnName("Contents_2");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chapter_Book");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CmtId);

                entity.ToTable("Comment");

                entity.Property(e => e.CmtId).HasColumnName("cmtID");

                entity.Property(e => e.BookId).HasColumnName("bookID");

                entity.Property(e => e.Cmt)
                    .HasMaxLength(500)
                    .HasColumnName("cmt");

                entity.Property(e => e.Like).HasColumnName("like");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("date")
                    .HasColumnName("publishDate");

                entity.Property(e => e.Reply).HasColumnName("reply");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User1");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.RateId).HasColumnName("rateID");

                entity.Property(e => e.BookId).HasColumnName("bookID");

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rate_Book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rate_User");
            });

            modelBuilder.Entity<Reading>(entity =>
            {
                entity.ToTable("Reading");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bookid).HasColumnName("bookid");

                entity.Property(e => e.Chapterid).HasColumnName("chapterid");

                entity.Property(e => e.ReadingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("readingDate");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Readings)
                    .HasForeignKey(d => d.Bookid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reading_Book");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Readings)
                    .HasForeignKey(d => d.Chapterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reading_Chapter");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Readings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reading_User");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.Chapter)
                    .HasMaxLength(50)
                    .HasColumnName("chapter");

                entity.Property(e => e.Detail).HasColumnName("detail");

                entity.Property(e => e.Problem).HasColumnName("problem");

                entity.Property(e => e.ReplyStatus)
                    .HasMaxLength(100)
                    .HasColumnName("replyStatus ");

                entity.Property(e => e.ReportTime)
                    .HasColumnType("datetime")
                    .HasColumnName("reportTime");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Book");

                entity.HasOne(d => d.ProblemNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Problem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_ReportType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_User");
            });

            modelBuilder.Entity<ReportType>(entity =>
            {
                entity.HasKey(e => e.ReportId);

                entity.ToTable("ReportType");

                entity.Property(e => e.ReportId)
                    .ValueGeneratedNever()
                    .HasColumnName("reportId");

                entity.Property(e => e.ReportType1)
                    .HasMaxLength(50)
                    .HasColumnName("reportType");
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.ToTable("Response");

                entity.Property(e => e.ResponseId).HasColumnName("responseId");

                entity.Property(e => e.Detail).HasColumnName("detail");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.Property(e => e.ResponseTime)
                    .HasColumnType("datetime")
                    .HasColumnName("responseTime");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Responses)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK_Response_Report");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Responses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Response_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("roleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<SavedBook>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SavedBook");

                entity.Property(e => e.BookId).HasColumnName("bookID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SavedBook_Book");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SavedBook_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.Transaction).HasColumnName("transaction");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
