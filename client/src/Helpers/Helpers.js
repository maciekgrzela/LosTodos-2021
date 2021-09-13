const groupByDate = (array) =>
  array.reduce((object, arrayElement) => {
    object[arrayElement['created'].substr(0, 10)] =
      object[arrayElement['created'].substr(0, 10)] === undefined
        ? []
        : object[arrayElement['created'].substr(0, 10)];
    arrayElement.todos = sortByDate(arrayElement.todos);
    object[arrayElement['created'].substr(0, 10)].push(arrayElement);
    return object;
  }, {});

const sortByDate = (array) => {
  return array.length > 0
    ? array.sort((a, b) => new Date(b.created) - new Date(a.created))
    : array;
};

export { groupByDate, sortByDate };
