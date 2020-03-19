function calculatePaginationOffset(currentPageNumber, numberOfItemsPerPage) {
  return (
    (Math.floor(
      (currentPageNumber * numberOfItemsPerPage) / numberOfItemsPerPage
    ) -
      1) *
    numberOfItemsPerPage
  );
}

export default calculatePaginationOffset;
