namespace StoryFlow.Helpers
{
    public class GeminiSchemaGenerator
    {
        public object GenerateBlogPostSchema()
        {
            return new
            {
                type = "object",
                properties = new
                {
                    slug = new { type = "string" },
                    metaTitle = new { type = "string" },
                    metaDescription = new { type = "string" },
                    summary = new { type = "string" },
                },
                required = new[]
                {
                    "slug",
                    "metaTitle",
                    "metaDescription",
                    "summary"
        }
            };
        }
        public object GenerateStorySchema()
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

        public object GenerateStoryQuizSchema()
        {
            return new
            {
                type = "object",
                properties = new
                {
                    questions = new
                    {
                        type = "array",
                        minItems = 4,
                        maxItems = 4,
                        items = new
                        {
                            type = "object",
                            properties = new
                            {
                                question = new
                                {
                                    type = "string"
                                },

                                answers = new
                                {
                                    type = "array",
                                    minItems = 4,
                                    maxItems = 4,
                                    items = new
                                    {
                                        type = "string"
                                    }
                                },

                                correctAnswerIndex = new
                                {
                                    type = "integer"
                                }
                            },
                            required = new[]
                            {
                        "question",
                        "answers",
                        "correctAnswerIndex"
                    }
                        }
                    }
                },

                required = new[]
                {
            "questions"
        }
            };
        }
    }
}