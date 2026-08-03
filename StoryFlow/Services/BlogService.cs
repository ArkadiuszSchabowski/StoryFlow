using AutoMapper;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;

        public BlogService(IMapper mapper, IBlogRepository blogRepository)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
        }
        public async Task<List<GetBlogPostDto>> Get()
        {
            var blogPosts = await _blogRepository.Get();

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
