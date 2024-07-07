import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ResearchLineService } from '../research-line.service'; // Importa el servicio

@Component({
  selector: 'app-filter-line',
  standalone: true,
  templateUrl: './filter-line.component.html',
  styleUrls: ['./filter-line.component.scss'],
  imports: [CommonModule, FormsModule, TranslateModule],
  providers: [ResearchLineService] // Provee el servicio en el componente
})
export class FilterLineComponent implements OnInit {
  lines: { id: string, name: string }[] = [];
  selectedLineId: string = 'RESEGTY';

  @Output() lineChanged = new EventEmitter<string>();

  constructor(private researchLineService: ResearchLineService) {}

  ngOnInit(): void {
    this.researchLineService.getResearchLines().subscribe(data => {
      this.lines = data;
    });
  }

  onLineChange(): void {
    console.log(this.selectedLineId+ 'esto se guarda');
    this.lineChanged.emit(this.selectedLineId);
  }
}
