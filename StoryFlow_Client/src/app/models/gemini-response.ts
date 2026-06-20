export class GeminiResponse {
  candidates!: {
    content: {
      parts: {
        text: string;
      }[];
    };
  }[];
}