export function showCategory(category: any) {
  switch (category) {
    case 0:
      return 'Zwierzęta';
    case 1:
      return 'Zdrowie';
    case 2:
      return 'Technologia';
    case 3:
      return 'Sport';
    case 4:
      return 'Sztuka';
    case 5:
      return 'Historia';
    case 6:
      return 'Muzyka';
    default:
      return 'Nieznany';
  }
}
  
  export function showSize(size: any) {
    switch (size) {
      case 0:
        return 'Krótka';
      case 1:
        return 'Średnia';
      case 2:
        return 'Długa';
      default:
        return 'Nieznana długość';
    }
  }

  export function showLanguageLevel(level: any) {
  switch (level) {
    case 0:
      return 'A1 - Początkujący';
    case 1:
      return 'A2 - Podstawowy';
    case 2:
      return 'B1 - Średnio zaawansowany';
    case 3:
      return 'B2 - Wyższy średnio zaawansowany';
    case 4:
      return 'C1 - Zaawansowany';
    case 5:
      return 'C2 - Biegły';
    default:
      return 'Nieznany';
  }
}

  export function showLanguageLevelShort(level: any) {
    switch (level) {
      case 0:
        return 'A1';

      case 1:
        return 'A2';

      case 2:
        return 'B1';

      case 3:
        return 'B2';

      case 4:
        return 'C1';

      case 5:
        return 'C2';

      default:
        return 'unkown';
    }
  }