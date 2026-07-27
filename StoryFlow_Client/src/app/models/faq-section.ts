import { FaqItem } from "./fq-item";

export interface FaqSection {
  id: string;
  title: string;
  items: FaqItem[];
}