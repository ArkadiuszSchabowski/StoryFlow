import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: '',
    renderMode: RenderMode.Prerender,
  },
  {
    path: 'faq',
    renderMode: RenderMode.Prerender,
  },
  {
    path: 'sezon/:id',
    renderMode: RenderMode.Server,
  },
  {
    path: 'text/:id',
    renderMode: RenderMode.Server,
  },
  {
    path: 'login',
    renderMode: RenderMode.Prerender,
  },
  {
    path: 'register',
    renderMode: RenderMode.Prerender,
  },
  {
    path: 'error-page',
    renderMode: RenderMode.Prerender,
  },
  {
    path: 'sezon-selection',
    renderMode: RenderMode.Client,
  },
  {
    path: 'profile',
    renderMode: RenderMode.Client,
  },
  {
    path: 'create-story',
    renderMode: RenderMode.Client,
  },
  {
    path: 'create-quiz',
    renderMode: RenderMode.Client,
  },
  {
    path: 'library',
    renderMode: RenderMode.Client,
  },
  {
    path: '**',
    renderMode: RenderMode.Client,
  },
];
