import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { GenreService } from '../../genre';

@Component({
  selector: 'app-genre-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './genre-form.html',
  styleUrl: './genre-form.scss'
})
export class GenreFormComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private genreService = inject(GenreService);
  private fb = inject(FormBuilder);

  genreForm!: FormGroup;
  genreId?: number;
  isEditMode = false;

  ngOnInit(): void {
    this.genreId = +this.route.snapshot.paramMap.get('id')!;
    this.isEditMode = !!this.genreId;

    this.genreForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]]
    });

    if (this.isEditMode) {
      this.loadGenreData();
    }
  }

  loadGenreData(): void {
    this.genreService.getById(this.genreId!).subscribe(genre => {
      this.genreForm.patchValue(genre);
    });
  }

  onSubmit(): void {
    if (this.genreForm.invalid) {
      return;
    }

    const formValue = this.genreForm.value;

    if (this.isEditMode) {
      this.genreService.update(this.genreId!, formValue).subscribe(() => {
        this.router.navigate(['/genres']);
      });
    } else {
      this.genreService.create(formValue).subscribe(() => {
        this.router.navigate(['/genres']);
      });
    }
  }
}
