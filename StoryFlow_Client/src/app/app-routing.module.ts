import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/public/home/home.component';
import { TextSelectionComponent } from './components/auth/text-selection/text-selection.component';
import { ProfileComponent } from './components/auth/profile/profile.component';
import { LoginComponent } from './components/public/login/login.component';
import { RegisterComponent } from './components/public/register/register.component';
import { TextDisplayComponent } from './components/auth/text-display/text-display.component';
import { ErrorPageComponent } from './components/system/error-page/error-page.component';
import { StoryCreateComponent } from './components/auth/moderator/story-create/story-create.component';
import { QuizCreateComponent } from './components/auth/moderator/quiz-create/quiz-create.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'text-selection', component: TextSelectionComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'text/:id', component: TextDisplayComponent },
  {path: 'error-page', component: ErrorPageComponent},
  {path: 'create-story', component: StoryCreateComponent},
  {path: 'create-quiz', component: QuizCreateComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
