using AutoMapper;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogValidator _blogValidator;

        public BlogService(IMapper mapper, IBlogRepository blogRepository, IBlogValidator blogValidator)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _blogValidator = blogValidator;
        }

        public async Task Add(AddBlogPostDto dto)
        {
            _blogValidator.Validate(dto);

            BlogPost blogPost = _mapper.Map<BlogPost>(dto);

            await _blogRepository.Add(blogPost);
        }

        public async Task AddBlogPostSection(AddBlogPostSectionDto dto)
        {
            _blogValidator.ValidateBlogPostSection(dto);

            BlogPostSection blogPostSection = _mapper.Map<BlogPostSection>(dto);

            await _blogRepository.AddBlogPostSection(blogPostSection);
        }

        public async Task<List<GetBlogPostDto>> Get()
        {
            var blogPosts = await _blogRepository.Get();

            List<GetBlogPostDto> dto = _mapper.Map<List<GetBlogPostDto>>(blogPosts);

            return dto;
        }

        public async Task<List<GetBlogPostDto>> GetVisibleBlogPosts()
        {
            var blogPosts = await _blogRepository.GetVisibleBlogPosts();

            List<GetBlogPostDto> dto = _mapper.Map<List<GetBlogPostDto>>(blogPosts);

            return dto;
        }

        public async Task<GetBlogPostDto> GetBySlug(string slug)
        {
            var blogPost = await _blogRepository.GetBySlug(slug);

            if(blogPost == null)
            {
                throw new NotFoundException("Nie znaleziono artykułu o podanym adresie URL");
            }

            GetBlogPostDto dto = _mapper.Map<GetBlogPostDto>(blogPost);

            return dto;
        }
    }
}
