import { Component, OnInit, inject } from '@angular/core';
import { Author, AuthorService } from '../../author';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-author-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './author-list.html',
  styleUrl: './author-list.scss'
})
export class AuthorListComponent implements OnInit {
  private authorService = inject(AuthorService);

  authors: Author[] = [];
  isLoading = true;

  ngOnInit(): void {
    this.loadAuthors();
  }

  loadAuthors(): void {
    this.isLoading = true;
    this.authorService.getAll().subscribe({
      next: (data) => {
        this.authors = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Erro ao carregar autores', err);
        this.isLoading = false;
      }
    });
  }

  deleteAuthor(id: number): void {
    if (confirm('Tem certeza que deseja excluir este autor?')) {
      this.authorService.delete(id).subscribe({
        next: () => {
          this.authors = this.authors.filter(a => a.id !== id);
        },
        error: (err) => {
          console.error('Erro ao excluir autor', err);
        }
      });
    }
  }
}
