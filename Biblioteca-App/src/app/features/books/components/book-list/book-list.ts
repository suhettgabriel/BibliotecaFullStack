import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Book, BookService } from '../../book';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './book-list.html',
  styleUrl: './book-list.scss'
})
export class BookListComponent implements OnInit {
  private bookService = inject(BookService);

  books: Book[] = [];
  isLoading = true;

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.isLoading = true;
    this.bookService.getAll().subscribe({
      next: (books) => {
        this.books = books;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Erro ao carregar livros', err);
        this.isLoading = false;
      }
    });
  }

  deleteBook(id: number): void {
    if (confirm('Tem certeza que deseja excluir este livro?')) {
      this.bookService.delete(id).subscribe({
        next: () => {
          this.books = this.books.filter(b => b.id !== id);
        },
        error: (err) => {
          console.error('Erro ao excluir livro', err);
        }
      });
    }
  }
}
