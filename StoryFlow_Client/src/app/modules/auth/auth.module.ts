import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizCreateComponent } from 'src/app/components/auth/moderator/quiz-create/quiz-create.component';
import { ProfileComponent } from 'src/app/components/auth/profile/profile.component';
import { StoryCreateComponent } from 'src/app/components/auth/moderator/story-create/story-create.component';
import { TextDisplayComponent } from 'src/app/components/auth/text-display/text-display.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LottieModule } from 'ngx-lottie';
import { playerFactory } from 'src/app/config/lottie.config';
import { LibraryPreviewComponent } from 'src/app/components/auth/moderator/library-preview/library-preview.component';

@NgModule({
  declarations: [
    QuizCreateComponent,
    ProfileComponent,
    StoryCreateComponent,
    TextDisplayComponent,
    LibraryPreviewComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    LottieModule.forRoot({ player: playerFactory }),
  ],
  exports: [
    QuizCreateComponent,
    ProfileComponent,
    StoryCreateComponent,
    TextDisplayComponent,
    LibraryPreviewComponent
  ],
})
export class AuthModule {}
