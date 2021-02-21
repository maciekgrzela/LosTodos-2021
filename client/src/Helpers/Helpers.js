const groupByDate = (array) => {
  let result = array.reduce((object, arrayElement) => {
    object[arrayElement['created'].substr(0, 10)] =
      object[arrayElement['created'].substr(0, 10)] === undefined
        ? []
        : object[arrayElement['created'].substr(0, 10)];
    arrayElement.tasks = sortByDate(arrayElement.tasks);
    object[arrayElement['created'].substr(0, 10)].push(arrayElement);
    return object;
  }, {});

  return result;
};

const sortByDate = (array) => {
  let result = array.sort((a, b) => new Date(b.created) - new Date(a.created));
  return result;
};

export { groupByDate, sortByDate };
