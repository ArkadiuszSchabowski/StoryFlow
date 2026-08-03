import { Component, OnInit } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { FAQ } from 'src/app/constants/faq';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  styleUrl: './faq.component.scss',
})
export class FaqComponent implements OnInit {
  readonly faq = FAQ;
  private openItems = new Set<string>();

  constructor(private meta: Meta) {}

  ngOnInit(): void {
    this.setDescription();
  }

  setDescription() {
    this.meta.updateTag({
      name: 'description',
      content:
        'Odpowiedzi na najczęstsze pytania o StoryFlow: jak wygląda nauka angielskiego z kotką Luną, dla kogo jest aplikacja i jak dobrać poziom trudności.'
    });
  }

  toggle(sectionId: string, question: string): void {
    const key = this.getKey(sectionId, question);
    if (this.openItems.has(key)) {
      this.openItems.delete(key);
    } else {
      this.openItems.add(key);
    }
  }

  isOpen(sectionId: string, question: string): boolean {
    return this.openItems.has(this.getKey(sectionId, question));
  }

  private getKey(sectionId: string, question: string): string {
    return `${sectionId}__${question}`;
  }
}
