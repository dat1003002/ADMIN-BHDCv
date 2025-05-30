// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
$(document).ready(function () {
  $('#datatable').DataTable({
    "paging": true,
    "searching": true,
    "ordering": true,
    "lengthChange": false,
    "pageLength": 10,
    "order": [[0, "asc"]],
    "info": "",
    "language": {
      "paginate": {
        "previous": "Trước",
        "next": "Sau"
      },
      "lengthMenu": "",
      "info": "",
      "search": "",
      "searchPlaceholder": "Tìm kiếm..."
    }
  });
});
