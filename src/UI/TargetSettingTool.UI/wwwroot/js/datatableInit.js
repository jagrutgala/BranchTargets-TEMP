$("[data-getAllTable]").DataTable({
  columnDefs: [
    { orderable: false, targets: "no-sort" },
    { visible: false, targets: "hide" },
  ],
  scrollX: true,
  sScrollXInner: "100%",
});
