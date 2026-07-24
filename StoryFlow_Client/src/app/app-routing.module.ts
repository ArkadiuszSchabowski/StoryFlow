import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/public/home/home.component';
import { ProfileComponent } from './components/auth/profile/profile.component';
import { TextDisplayComponent } from './components/auth/text-display/text-display.component';
import { StoryCreateComponent } from './components/auth/moderator/story-create/story-create.component';
import { QuizCreateComponent } from './components/auth/moderator/quiz-create/quiz-create.component';
import { SezonComponent } from './components/auth/sezon/sezon.component';
import { SezonSelectionComponent } from './components/auth/sezon-selection/sezon-selection.component';
import { LibraryPreviewComponent } from './components/auth/moderator/library-preview/library-preview.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'StoryFlow - Nauka angielskiego z kotką Luną',
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./components/public/login/login.component').then(m => m.LoginComponent),
    title: 'Logowanie - StoryFlow',
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./components/public/register/register.component').then(m => m.RegisterComponent),
    title: 'Rejestracja - StoryFlow',
  },
  {
    path: 'error-page',
    loadComponent: () =>
      import('./components/public/error-page/error-page.component').then(m => m.ErrorPageComponent),
    title: 'Błąd - StoryFlow',
  },
  {
    path: 'sezon-selection',
    component: SezonSelectionComponent,
    title: 'Wybór sezonu - StoryFlow',
  },
  {
    path: 'profile',
    component: ProfileComponent,
    title: 'Profil użytkownika - StoryFlow',
  },
  {
    path: 'create-story',
    component: StoryCreateComponent,
    title: 'Tworzenie historii - StoryFlow',
  },
  {
    path: 'create-quiz',
    component: QuizCreateComponent,
    title: 'Tworzenie quizu - StoryFlow',
  },
  {
    path: 'library',
    component: LibraryPreviewComponent,
    title: 'Biblioteka historii - StoryFlow',
  },
  {
    path: 'sezon/:id',
    component: SezonComponent,
    title: 'Podgląd sezonu - StoryFlow',
  },
  {
    path: 'text/:id',
    component: TextDisplayComponent,
    title: 'Historia - StoryFlow',
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
