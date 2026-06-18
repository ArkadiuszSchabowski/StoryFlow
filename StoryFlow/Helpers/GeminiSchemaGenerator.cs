namespace StoryFlow.Helpers
{
    public class GeminiSchemaGenerator
    {
        public object GenerateSchema()
        {
            return new
            {
                type = "object",
                properties = new
                {
                    polishStory = new { type = "string" },
                    englishStory = new { type = "string" },
                    polishTitle = new { type = "string" },
                    englishTitle = new { type = "string" },
                    polishDescription = new { type = "string" },
                    englishDescription = new { type = "string" },

                    storyCategory = new
                    {
                        type = "string",
                        @enum = new[]
                        {
                            "Animals",
                            "Health",
                            "Technology",
                            "Sport",
                            "Art",
                            "History",
                            "Music"
                        }
                    },

                    languageLevel = new
                    {
                        type = "string",
                        @enum = new[]
                        {
                            "A1",
                            "A2",
                            "B1",
                            "B2",
                            "C1",
                            "C2"
                        }
                    }
                },
                required = new[]
                {
                    "polishStory",
                    "englishStory",
                    "polishTitle",
                    "englishTitle",
                    "polishDescription",
                    "englishDescription",
                    "storyCategory",
                    "languageLevel"
                }
            };
        }
    }
}
