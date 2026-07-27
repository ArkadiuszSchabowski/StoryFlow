import { Component } from '@angular/core';
import { FAQ } from 'src/app/constants/faq';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  styleUrl: './faq.component.scss',
})
export class FaqComponent {
  readonly faq = FAQ;

  private openItems = new Set<string>();

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