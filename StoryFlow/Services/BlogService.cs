using AutoMapper;
using Microsoft.Extensions.Options;
using StoryFlow.Exceptions;
using StoryFlow.Helpers;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Models;
using System.Text;
using System.Text.Json;

namespace StoryFlow.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogValidator _blogValidator;
        private readonly GeminiSchemaGenerator _geminiSchemaGenerator;
        private readonly GeminiSettings _geminiSettings;

        public BlogService(IMapper mapper, IBlogRepository blogRepository, IBlogValidator blogValidator, IOptions<GeminiSettings> geminiSettings, GeminiSchemaGenerator geminiSchemaGenerator)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _blogValidator = blogValidator;
            _geminiSchemaGenerator = geminiSchemaGenerator;
            _geminiSettings = geminiSettings.Value;
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

        public async Task<string> Generate(GenerateBlogPostDto dto)
        {
            object schema = _geminiSchemaGenerator.GenerateBlogPostSchema();

            var instructions = string.IsNullOrWhiteSpace(dto.Instructions)
                ? ""
                : $""" 
        Instructions:
        - {dto.Instructions}
        """;

            using var client = new HttpClient
            {
                BaseAddress = new Uri(_geminiSettings.BaseUrl)
            };


            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = $"""
                        You must write EVERYTHING in Polish (title, metaTitle, metaDescription, summary, body). Do not use English anywhere in the output.

                        I'm building "StoryFlow" — an app for learning English through stories.
                        Create a blog post that is SEO-optimized and reader-friendly.
                        
                        SEO requirements:
                        - Naturally include the main keyword/topic in both the meta title and meta description
                        - Meta title: phrase it as a question or a clear reader problem/goal (matching real search intent), not a generic marketing claim
                        - Meta description: a direct expansion of the meta title — briefly answer or elaborate on what the title promises
                        - Avoid keyword stuffing — keep it natural and readable

                        Requirements:
                        -The meta title cannot exceed 50 characters
                        -The meta description cannot exceed 115 characters
                        
                        Summary requirements:
                        - At least 6 sentences
                        - Briefly summarize the key takeaways from the post (not an introduction)

                        Rules:
                        {instructions}

                        Return ONLY JSON that matches the responseSchema.
                        """
                    }
                }
            }
        },
                generationConfig = new
                {
                    responseMimeType = "application/json",
                    responseSchema = schema
                }
            };


            var json = JsonSerializer.Serialize(requestBody);

            var response = await client.PostAsync(
                $"/v1beta/models/{_geminiSettings.ModelName}:generateContent?key={_geminiSettings.ApiKey}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            var geminiResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine(geminiResponse);

            return geminiResponse;
        }
    }
}
