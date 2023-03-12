export const calculateMessagesCountForPage = (number) => {
    const ranges = [
      [300, 400, 4],
      [400, 500, 5],
      [500, 600, 7],
      [600, 700, 7],
      [700, 800, 9],
      [800, 900, 10],
    ];
  
    for (let i = 0; i < ranges.length; i++) {
      const [start, end, coefficient] = ranges[i];
      if (number >= start && number < end) {
        return coefficient;
      }
    }
    return null; // якщо число не входить в жоден проміжок
  };