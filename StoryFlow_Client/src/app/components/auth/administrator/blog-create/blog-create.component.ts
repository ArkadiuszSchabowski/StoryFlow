import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ToastrService } from 'ngx-toastr';
import { BlogService } from 'src/app/_services/blog.service';
import { ThemeService } from 'src/app/_services/theme.service';
import { AddBlogPostDto } from 'src/app/models/add-blog-post-dto';
import { GenerateBlogPostDto } from 'src/app/models/generate-blog-post-dto';

@Component({
  selector: 'app-blog-create',
  imports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './blog-create.component.html',
  styleUrl: './blog-create.component.scss',
})
export class BlogCreateComponent {

  constructor(
    private fb: FormBuilder,
    private blogService: BlogService,
    private toastr: ToastrService,
    public themeService: ThemeService,
  ) {}

  generateForm: any = this.fb.group({
    instructions: '',
  });

  addForm: any = this.fb.group({
    slug: [''],
    metaTitle: [''],
    metaDescription: [''],
    summary: [''],
  });

  public generate() {
    console.log("generate works!")
    const dto: GenerateBlogPostDto = {
      instructions: this.generateForm.get('instructions').value,
    };

    console.log(dto);

      this.blogService.generate(dto).subscribe({
        next: (response) => {
          this.addForm.patchValue({
            slug: response.slug,
            metaTitle: response.metaTitle,
            metaDescription: response.metaDescription,
            summary: response.summary,
          });
        },
        error: () => {},
      });
    }

  public add() {
    const dto: AddBlogPostDto = {
      slug: this.addForm.get('slug')?.value,
      metaTitleContent: this.addForm.get('metaTitle')?.value,
      metaTitleDescription: this.addForm.get('metaDescription')?.value,
      summary: this.addForm.get('summary')?.value,
    };

    this.blogService.add(dto).subscribe({
      next: () => {
        this.toastr.success('Wpis został dodany.');
        this.addForm.reset();
        this.generateForm.patchValue({
          instructions: '',
        });
      },
      error: (error) => {
        this.toastr.error(error.error);
      },
    });
  }
}