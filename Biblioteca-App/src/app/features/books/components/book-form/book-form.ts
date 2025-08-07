import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { BookService } from '../../book';
import { AuthorService } from '../../../authors/author';
import { GenreService } from '../../../genres/genre';
import { Author } from '../../../authors/author';
import { Genre } from '../../../genres/genre';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './book-form.html',
  styleUrl: './book-form.scss'
})
export class BookFormComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private bookService = inject(BookService);
  private authorService = inject(AuthorService);
  private genreService = inject(GenreService);
  private fb = inject(FormBuilder);

  bookForm!: FormGroup;
  bookId?: number;
  isEditMode = false;
  authors: Author[] = [];
  genres: Genre[] = [];

  ngOnInit(): void {
    this.bookId = +this.route.snapshot.paramMap.get('id')!;
    this.isEditMode = !!this.bookId;

    this.bookForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      year: ['', [Validators.required, Validators.min(1000), Validators.max(new Date().getFullYear())]],
      authorId: ['', Validators.required],
      genreId: ['', Validators.required]
    });

    this.loadAuthors();
    this.loadGenres();

    if (this.isEditMode) {
      this.loadBookData();
    }
  }

  loadBookData(): void {
    this.bookService.getById(this.bookId!).subscribe(book => {
      this.bookForm.patchValue(book);
    });
  }

  loadAuthors(): void {
    this.authorService.getAll().subscribe(authors => {
      this.authors = authors;
    });
  }

  loadGenres(): void {
    this.genreService.getAll().subscribe(genres => {
      this.genres = genres;
    });
  }

  onSubmit(): void {
    if (this.bookForm.invalid) {
      return;
    }

    const formValue = this.bookForm.value;

    if (this.isEditMode) {
      this.bookService.update(this.bookId!, formValue).subscribe(() => {
        this.router.navigate(['/books']);
      });
    } else {
      this.bookService.create(formValue).subscribe(() => {
        this.router.navigate(['/books']);
      });
    }
  }
}
