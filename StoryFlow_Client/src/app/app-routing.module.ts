import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/public/home/home.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'StoryFlow - Nauka angielskiego z kotką Luną',
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./components/public/login/login.component').then(
        (m) => m.LoginComponent,
      ),
    title: 'Logowanie - StoryFlow',
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./components/public/register/register.component').then(
        (m) => m.RegisterComponent,
      ),
    title: 'Rejestracja - StoryFlow',
  },
  {
    path: 'error-page',
    loadComponent: () =>
      import('./components/public/error-page/error-page.component').then(
        (m) => m.ErrorPageComponent,
      ),
    title: 'Błąd - StoryFlow',
  },
  {
    path: 'sezon-selection',
    loadComponent: () =>
      import('./components/auth/sezon-selection/sezon-selection.component').then(
        (m) => m.SezonSelectionComponent,
      ),
    title: 'Wybór sezonu - StoryFlow',
  },
  {
    path: 'profile',
    loadComponent: () =>
      import('./components/auth/profile/profile.component').then(
        (m) => m.ProfileComponent,
      ),
    title: 'Profil użytkownika - StoryFlow',
  },
  {
    path: 'create-story',
    loadComponent: () =>
      import('./components/auth/moderator/story-create/story-create.component').then(
        (m) => m.StoryCreateComponent,
      ),
    title: 'Tworzenie historii - StoryFlow',
  },
  {
    path: 'create-quiz',
    loadComponent: () =>
      import('./components/auth/moderator/quiz-create/quiz-create.component').then(
        (m) => m.QuizCreateComponent,
      ),
    title: 'Tworzenie quizu - StoryFlow',
  },
  {
    path: 'library',
    loadComponent: () =>
      import('./components/auth/moderator/library-preview/library-preview.component').then(
        (m) => m.LibraryPreviewComponent,
      ),
    title: 'Biblioteka historii - StoryFlow',
  },
  {
    path: 'sezon/:id',
    loadComponent: () =>
      import('./components/auth/sezon/sezon.component').then(
        (m) => m.SezonComponent,
      ),
    title: 'Podgląd sezonu - StoryFlow',
  },
  {
    path: 'text/:id',
    loadComponent: () =>
      import('./components/auth/text-display/text-display.component').then(
        (m) => m.TextDisplayComponent,
      ),
    title: 'Historia - StoryFlow',
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
