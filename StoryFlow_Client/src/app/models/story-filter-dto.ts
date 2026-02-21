import { LanguageLevel } from "../enums/language-level";
import { StoryCategory } from "../enums/story-category";
import { StorySize } from "../enums/story-size";

export interface StoryFilter {
    languageLevel: LanguageLevel | null;
    category: StoryCategory | null;
    size: StorySize | null;
}