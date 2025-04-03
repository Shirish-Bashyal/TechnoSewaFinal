using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Post;
using Application.Interfaces.Data;
using Application.Interfaces.User.Consumer;
using Application.Response;
using Domain.Entities.Application;
using Domain.Entities.User;
using Domain.Entities.User.PostDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services.User.Consumer
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostEnvironment _env;

        public PostService(
            IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IHostEnvironment env
        )
        {
            _uow = uow;
            _userManager = userManager;
            _env = env;
        }

        public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }
            var contentPath = _env.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(imageFile.FileName);
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException(
                    $"Only {string.Join(",", allowedFileExtensions)} are allowed."
                );
            }
            // generate a unique filename
            var fileName = $"{Guid.NewGuid().ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return fileName;
        }

        public async Task<ServiceResponse<object>> CreatePost(CreatePostDTO model, string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                Category? category = await _uow.AsyncRepositories<Category>()
                    .GetSingleBySpec(x => x.Name == model.Category);
                if (category != null)
                {
                    var post = new Post
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Lattitude = model.Lattitude,
                        Longitude = model.Longitude,
                        Status = 0,
                        User = user,
                        Category = category,
                    };
                    if (model.ImageFiles != null && model.ImageFiles.Any())
                    {
                        post.Photos = new List<PhotoPath>();
                        foreach (var pic in model.ImageFiles)
                        {
                            if (pic?.Length > 1 * 1024 * 1024)
                            {
                                return new ServiceResponse<object>()
                                {
                                    Success = false,
                                    Message = "File size shouldn't exist 1MB.",
                                };
                            }
                            if (pic != null)
                            {
                                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png", ".JPG"];
                                string createdImageName = await SaveFileAsync(
                                    pic,
                                    allowedFileExtentions
                                );

                                post.Photos.Add(new PhotoPath { Path = createdImageName });
                            }
                            else
                            {
                                return new ServiceResponse<object>
                                {
                                    Message = "Invalid picture",
                                    Success = false,
                                };
                            }
                        }

                        await _uow.AsyncRepositories<Post>().AddAsync(post);
                        var result = await _uow.Save();
                        if (result > 0)
                        {
                            return new ServiceResponse<object>
                            {
                                Message = "problem posted",
                                Success = true,
                            };
                        }
                        else
                        {
                            return new ServiceResponse<object>
                            {
                                Message = "operation error",
                                Success = false,
                            };
                        }
                    }
                    else
                    {
                        return new ServiceResponse<object>
                        {
                            Message = "Invalid Picture",
                            Success = false,
                        };
                    }
                }
                else
                {
                    return new ServiceResponse<object>
                    {
                        Message = $"{model.Category} Category not found",
                        Success = false,
                    };
                }
            }
            else
            {
                return new ServiceResponse<object> { Message = "User not found", Success = false, };
            }
        }

        public async Task<ServiceResponse<object>> GetPost(int PostId)
        {
            var includes = new Expression<Func<Post, object>>[]
            {
                s => s.User,
                s => s.Category,
                s => s.Photos,
            };
            var post = await _uow.AsyncRepositories<Post>()
                .GetWithIncludeAndFilter(includes, x => x.Id == PostId);
            if (post != null)
            {
                var images = new List<string>();
                var baseUrl = "https://localhost:7206";
                foreach (var pic in post.Photos)
                {
                    images.Add($"{baseUrl}/Resourses/{pic.Path}");
                }
                var result = new PostResponseDTO
                {
                    Title = post.Title,
                    Description = post.Description,
                    Category = post.Category.Name,
                    CreationDate = post.AddedDate,
                    Lattitude = post.Lattitude,
                    Longitude = post.Longitude,
                    UserName = post.User.UserName,
                    ImageUrl = images,
                };
                return new ServiceResponse<object> { Success = true, Data = result, };
            }
            else
            {
                return new ServiceResponse<object> { Success = false, Message = "Operation error" };
            }
        }

        public async Task<ServiceResponse<object>> GetAllPosts(string UserId)
        {
            var includes = new Expression<Func<Post, object>>[]
            {
                s => s.User,
                s => s.Category,
                s => s.Photos,
            };
            var posts = await _uow.AsyncRepositories<Post>()
                .GetListWithIncludeAndFilter(includes, x => x.User.Id == UserId);
            if (posts == null)
            {
                return new ServiceResponse<object>
                {
                    Success = true,
                    Message = "User have not posted anything"
                };
            }

            var baseUrl = "https://localhost:7206";

            var result = posts
                .Select(post => new PostResponseDTO
                {
                    Title = post.Title,
                    Description = post.Description,
                    Category = post.Category.Name,
                    CreationDate = post.AddedDate,
                    Lattitude = post.Lattitude,
                    Longitude = post.Longitude,
                    UserName = post.User.UserName,
                    ImageUrl = post.Photos.Select(photo => $"{baseUrl}/Resources/{photo.Path}")
                        .ToList()
                })
                .ToList();

            return new ServiceResponse<object> { Success = true, Data = result };
        }

        public Task<ServiceResponse<object>> DeletePost(int PostId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<object>> UpdatePost(int PostId)
        {
            throw new NotImplementedException();
        }
    }
}
