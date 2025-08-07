import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthorService } from '../../author';

@Component({
  selector: 'app-author-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './author-form.html',
  styleUrl: './author-form.scss'
})
export class AuthorFormComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private authorService = inject(AuthorService);
  private fb = inject(FormBuilder);

  authorForm!: FormGroup;
  authorId?: number;
  isEditMode = false;

  ngOnInit(): void {
    this.authorId = +this.route.snapshot.paramMap.get('id')!;
    this.isEditMode = !!this.authorId;

    this.authorForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]]
    });

    if (this.isEditMode) {
      this.loadAuthorData();
    }
  }

  loadAuthorData(): void {
    this.authorService.getById(this.authorId!).subscribe(author => {
      this.authorForm.patchValue(author);
    });
  }

  onSubmit(): void {
    if (this.authorForm.invalid) {
      return;
    }

    const formValue = this.authorForm.value;

    if (this.isEditMode) {
      this.authorService.update(this.authorId!, formValue).subscribe(() => {
        this.router.navigate(['/authors']);
      });
    } else {
      this.authorService.create(formValue).subscribe(() => {
        this.router.navigate(['/authors']);
      });
    }
  }
}
