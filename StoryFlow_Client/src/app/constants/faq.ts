import { FaqSection } from '../models/faq-section';

export const FAQ: FaqSection[] = [
  {
    id: 'faq-jak-dziala',
    title: 'Funkcje aplikacji StoryFlow',
    items: [
      {
        question: 'Jakie funkcje oferuje StoryFlow?',
        answer: `Aplikacja StoryFlow z kotką Luną to nowoczesny sposób na naukę
          angielskiego poprzez czytanie historii. Oferuje czytanie opowiadań
          po angielsku z możliwością tłumaczenia każdego zdania w tekście.
          Dzięki temu nie musisz odrywać się od historii i tracić skupienia,
          by sprawdzić znaczenie nieznanego słowa czy zwrotu. Po przeczytaniu
          historii możesz sprawdzić swoje umiejętności językowe w quizie
          dopasowanym do poziomu trudności danej opowieści. To także wygodny
          sposób na codzienną naukę angielskiego — kilka minut dziennie
          z krótką historią wystarczy, by regularnie rozwijać język.`,
      },
      {
        question: 'Jak zdobywać gwiazdki w StoryFlow?',
        answer: `Gwiazdki otrzymujemy w momencie odpowiedzenia na wszystkie pytania
          quizu po przeczytanej historii. Liczba zdobytych gwiazdek zależy od
          punktacji uwarunkowanej poziomem trudności oraz długością historii —
          im trudniejsza i dłuższa historia, tym więcej gwiazdek możesz zdobyć.`,
      },
      {
        question: 'Jak liczone są gwiazdki?',
        answer: `Liczba gwiazdek zależy od dwóch czynników: poziomu trudności historii
          oraz jej długości. Za każdą poprawną odpowiedź w quizie otrzymujesz
          punkty zależne od poziomu — od 10 punktów na poziomie A1, przez 15 na
          A2, 20 na B1, 25 na B2, 30 na C1, aż do 35 punktów na poziomie C2.
          Dodatkowo otrzymujesz bonus za ukończenie historii: 10 punktów za
          historię krótką, 20 punktów za średnią i 30 punktów za długą. To
          system, który motywuje do nauki angielskiego na coraz wyższym
          poziomie zaawansowania.`,
      },
      {
        question: 'Po co są bilety i jaka jest ich liczba?',
        answer: `Początkowo każdy zarejestrowany użytkownik StoryFlow otrzymuje
          cztery bilety. Dzięki biletom możesz odblokowywać kolejne historie
          do nauki angielskiego. Gdy zdecydujesz się rozpocząć historię, Twój
          bilet blokuje się tymczasowo na czas jej trwania. Jeśli zdobędziesz
          przynajmniej 50% wyniku w quizie po tej historii, bilet zostaje
          odblokowany i możesz użyć go ponownie.`,
      },
      {
        question: 'Mam zablokowane wszystkie bilety. Jak je odblokować?',
        answer: `W takim przypadku wejdź w historię, która została już przez Ciebie
          odkryta, i rozwiąż powiązany z nią quiz. Jeśli zdobędziesz
          przynajmniej 50% poprawnych odpowiedzi, bilet zostanie odblokowany
          i wróci do puli dostępnych biletów.`,
      },
      {
        question: 'Niektóre sezony są dla mnie zablokowane. Jak je odblokować?',
        answer: `Aby odblokować kolejny sezon historii w StoryFlow, musisz zdobyć określoną liczbę gwiazdek w poprzednich sezonach. Kolejne sezony wprowadzają różne kategorie tematyczne, dzięki czemu poszerzasz słownictwo w wielu obszarach — czasem na podobnym poziomie trudności, a czasem historie stają się bardziej wymagające. Dzięki temu systematycznie rozwijasz znajomość języka angielskiego, krok po kroku.`,
      },
    ],
  },
  {
    id: 'faq-cennik',
    title: 'Cennik i dostępność aplikacji',
    items: [
      {
        question: 'Czy aplikacja StoryFlow jest bezpłatna?',
        answer: `Tak, aplikacja StoryFlow do nauki angielskiego jest w pełni
          bezpłatna. Możesz korzystać ze wszystkich dostępnych funkcji —
          czytania historii, tłumaczeń zdań, quizów oraz systemu gwiazdek —
          bez żadnych ukrytych opłat czy subskrypcji. To dobra opcja, jeśli
          szukasz miejsca, gdzie można nauczyć się angielskiego za darmo.`,
      },
      {
        question: 'Czy mogę wesprzeć StoryFlow?',
        answer: `<div class="coffee-container">
        <div class=coffee-item>Tak! Jeśli podoba Ci się StoryFlow i chcesz pomóc mi go dalej rozwijać, możesz postawić mi wirtualną kawę. Dziękuję za wsparcie!</div>
        <img
        id="coffee-logo-faq"
        class="coffee-image"
        src="assets/faq/coffee.webp"
        alt="Postaw wirtualną kawę twórcy StoryFlow"
      width="150"
      height="150"
      />
        <a class="coffee-text"         
         [ngClass]="{
         'light-mode-background-primary light-mode-text': (themeService.theme$ | async),
         'dark-mode-background-primary dark-mode-text': !(themeService.theme$ | async)
          }" href="https://suppi.pl/storyflow" target="_blank" rel="noopener noreferrer">Postaw kawę</a></div>`,
      },
    ],
  },
  {
    id: 'faq-bezpieczenstwo',
    title: 'Bezpieczeństwo dziecka w StoryFlow',
    items: [
      {
        question: 'Czy aplikacja StoryFlow jest bezpieczna dla dzieci?',
        answer: `Tak, aplikacja StoryFlow jest w pełni bezpieczna dla dzieci i dobrze
          sprawdza się jako aplikacja do nauki angielskiego dla najmłodszych.
          Historie nie mają progu wiekowego, dzięki czemu z Luną i jej
          opowieściami może uczyć się cała rodzina — niezależnie od tego,
          kiedy dziecko zaczyna naukę angielskiego. Wiele historii zawiera
          morały, dzięki którym dziecko może przy okazji nauki języka
          przyswoić dobre wzorce zachowań.`,
      },
    ],
  },
  {
    id: 'faq-pomoc',
    title: 'Pomoc techniczna',
    items: [
      {
        question: 'Gdzie mogę zgłosić problem lub otrzymać pomoc techniczną?',
        answer: `Problemy techniczne związane z aplikacją StoryFlow możesz zgłaszać
          poprzez e-mail: storyflowlearning@gmail.com. Chętnie pomożemy w
          rozwiązaniu wszelkich trudności związanych z nauką angielskiego
          w naszej aplikacji.`,
      },
      {
        question:
          'Dlaczego strona czasem wolniej się uruchamia po dłuższej przerwie?',
        answer: `Aplikacja StoryFlow korzysta obecnie z darmowego planu bazy
    danych. Tego typu darmowe usługi hostingowe "usypiają" serwer po
    okresie bezczynności, dzięki czemu po dłuższej przerwie w
    odwiedzinach strona może wczytywać się kilka-kilkanaście sekund
    dłużej niż zwykle — to czas potrzebny na ponowne "wybudzenie"
    bazy danych. Kolejne odwiedziny w krótkim czasie powinny być już
    znacznie szybsze. Wraz z rosnącą liczbą użytkowników planujemy
    przejść na płatny plan hostingu, co całkowicie wyeliminuje ten
    problem.`,
      },
    ],
  },
];
