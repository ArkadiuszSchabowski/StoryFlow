import { ResolveFn } from "@angular/router";

export const blogTitleResolver: ResolveFn<string> = (route) => {
  const slug = route.paramMap.get('slug') ?? '';
  const formatted = slug
    .split('-')
    .join(' ');
  return formatted.charAt(0).toUpperCase() + formatted.slice(1);
};