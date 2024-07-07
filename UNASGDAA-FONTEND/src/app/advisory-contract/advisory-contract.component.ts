import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import { AdvisoryContractService } from '../advisory-contract.service';
import { FilterLineComponent } from '../filter-line/filter-line.component';

@Component({
  selector: 'app-advisory-contract',
  standalone: true,
  imports: [CommonModule, HttpClientModule,
    TranslateModule,FilterLineComponent], 
  templateUrl: './advisory-contract.component.html',
  styleUrls: ['./advisory-contract.component.scss']
})
export class AdvisoryContractComponent implements OnInit {
  advisoryContracts: any[] = [];
  selectedLine: string = 'ALL';
  includeDeleted: boolean = false;

  constructor(private advisoryContractService: AdvisoryContractService) {}

  ngOnInit(): void {
    this.loadContracts();
  }

  loadContracts(): void {
    this.advisoryContractService.getAdvisoryContracts(this.selectedLine, this.includeDeleted).subscribe(
      (response) => {
        console.log('Response:', response);
        this.advisoryContracts = response.data.items;
      },
      (error) => {
        console.error('Error fetching advisory contracts', error);
      }
    );
  }

  onLineChanged(line: string): void {
    this.selectedLine = line;
    this.loadContracts();
  }
}