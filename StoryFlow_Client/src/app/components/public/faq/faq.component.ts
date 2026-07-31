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
        'Najczęściej zadawane pytania o StoryFlow - jak działa aplikacja, quizy i historie do nauki angielskiego.',
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
