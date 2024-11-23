var dtable;
$(document).ready(function() {
    dtable = $('#myTable').DataTable({
        "ajax": { "url": "Admin/Product/AllProducts" },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function(data) {
                    return `
                            <a href="/Admin/Product/CreateUpdate?id=${data}"><i class="fa fa-pencil-square"></i></a>
<a onclick=RemoveProduct("/Admin/Product/Delete/${data}")><i class="fa fa-p"></i></a>
`
                }
            }
        ]
    });
});

function RemoveProduct(url) {
    swal.fire({
        title: 'Are You sure?',
        text: "'You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonText: 'Yes,delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'Delete',
                success: function(data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        Toast.success(data.message);
                    } else {
                        Toast.error(data.message);
                    }
                }
            });
        }
    });
}