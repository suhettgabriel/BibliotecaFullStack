import { Component, OnInit, inject } from '@angular/core';
import { Genre, GenreService } from '../../genre';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-genre-list',
  standalone: true,
  imports: [CommonModule],
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
    this.genreService.getAll().subscribe({
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
}
