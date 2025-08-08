import { Component, OnInit, inject } from '@angular/core';
import { Genre, GenreService } from '../../genre';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-genre-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './genre-list.html',
  styleUrl: './genre-list.scss'
})
export class GenreListComponent implements OnInit {
  private genreService = inject(GenreService);

  genres: Genre[] = [];
  isLoading = true;

  ngOnInit(): void {
    this.loadGenres();
  }

  loadGenres(): void {
    this.isLoading = true;
    this.genreService.getAllWithBookCount().subscribe({
      next: (data) => {
        this.genres = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Erro ao carregar gêneros', err);
        this.isLoading = false;
      }
    });
  }

  deleteGenre(id: number): void {
    if (confirm('Tem certeza que deseja excluir este gênero?')) {
      this.genreService.delete(id).subscribe({
        next: () => {
          this.genres = this.genres.filter(g => g.id !== id);
        },
        error: (err) => {
          console.error('Erro ao excluir gênero', err);
        }
      });
    }
  }
}
